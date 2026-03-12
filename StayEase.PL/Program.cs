
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StayEase.BLL.Service;
using StayEase.DAL.Data;
using StayEase.DAL.Models;
using StayEase.DAL.Repository;
using StayEase.DAL.Utilits;
using System.Globalization;
using System.Text;

namespace StayEase.PL
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();

            builder.Services.AddOpenApi();


            builder.Services.AddLocalization(options => options.ResourcesPath = "");

            const string defaultCulture = "en";
            var supportedCultures = new[]
            {
                         new CultureInfo(defaultCulture),
                           new CultureInfo("ar")
                             };

            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture(defaultCulture);
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;

                options.RequestCultureProviders.Clear();
                options.RequestCultureProviders.Add(new QueryStringRequestCultureProvider()
                {
                    QueryStringKey = "lang",
                });

            });

            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IHotelRepository, HotelRepository>();

            builder.Services.AddScoped<IHotelService, HotelService>();

            builder.Services.AddScoped<ISeedData, RoleSeedData>();
            builder.Services.AddScoped<ISeedData, UserSeedData>();

            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

            builder.Services.AddTransient<IEmailSender, EmailSender>();


            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
     .AddJwtBearer(options =>
   {
       options.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuer = true,
           ValidateAudience = true,
           ValidateLifetime = true,
           ValidateIssuerSigningKey = true,
           ValidIssuer = builder.Configuration["JWT:Issuer"],
           ValidAudience = builder.Configuration["JWT:Audience"],
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]!))
       };
   });


            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "StayEase API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Enter: Bearer {your token}"
                });

                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
            });


            var app = builder.Build();
            app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var seeders = services.GetServices<ISeedData>();
                foreach (var seeder in seeders)
                {
                    await seeder.DataSeed();
                }
            }



            app.MapControllers();

            app.Run();
        }
    }
}
