namespace APIApp
{
    using APIApp.Repositories.JWT;
    using APIApp.Services;
    using APIApp.Services.Authentication;
    using APIApp.Services.JWT;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.IdentityModel.Tokens;
    using OlxDataAccess.Admins.Repository;
    using OlxDataAccess.Categories.Repositories;
    using OlxDataAccess.Companies.Repositories;
    using OlxDataAccess.DBContext;
    using OlxDataAccess.Fields.Repositories;
    using OlxDataAccess.Governorates.Repositories;
    using OlxDataAccess.Models;
    using OlxDataAccess.Users.Repositories;
    using System.Security.Claims;
    using System.Text;
    using System.Text.Json.Serialization;

    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

            #region Cors
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder
                    .WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });

            #endregion

            #region Json Serializer
            builder.Services.AddMvc()
                .AddJsonOptions(option =>
                {
                    option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                });
            #endregion

            #region Connection
            // Add services to the container.


            builder.Services.AddDbContext<OLXContext>(db =>
       db.UseSqlServer(
           builder.Configuration.GetConnectionString("conn")
           )
       );


            #endregion

            #region JWT
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                    RoleClaimType = ClaimTypes.Role,

                };
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin",
                    policy => policy.RequireClaim("Admin"));
                options.AddPolicy("User",
                    policy => policy.RequireRole("User"));
                options.AddPolicy("Admin,User", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole("Admin", "User");
                });
            });
            #endregion

            #region Dependency Injection
            builder.Services.AddScoped<IJWT, JWTRepository>();
            builder.Services.AddScoped<IAdminRepository, AdminRepository>();
            builder.Services.AddScoped<IAuthentication<Admin>, AdminRepository>();
            builder.Services.AddScoped<IAuthentication<User>, UserRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IFieldRepository, FieldRepository>();
            builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
            builder.Services.AddScoped<UserAuthentication>();
            builder.Services.AddScoped<IGovernorateRepository, GovernorateRepository>();
            #endregion

            #region AutoMapper
            builder.Services.AddAutoMapper(typeof(Program));
            #endregion

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();



            WebApplication? app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            #region User Authentication
            app.UseAuthentication();
            #endregion

            app.UseAuthorization();

            app.MapControllers();

            #region Use Cors
            app.UseCors(policyName: "AllowAll");
            #endregion

            app.Run();
        }
    }
}