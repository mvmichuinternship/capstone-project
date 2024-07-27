using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using RealEstateAPI.Context;
using RealEstateAPI.Interfaces;
using RealEstateAPI.Models;
using RealEstateAPI.Repositories;
using RealEstateAPI.Services;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;


namespace RealEstateAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(option =>
            {
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
            });

            #region Authentication
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey:JWT"]))
                    };

                });
            #endregion

            #region CORS
            builder.Services.AddCors(opts =>
            {
                opts.AddPolicy("MyCors", options =>
                {
                    options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });
            #endregion

            #region context
            builder.Services.AddDbContext<RealEstateAppContext>(
               options => options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"))
               );
            #endregion

            #region repositories
            builder.Services.AddScoped<IRepository<string, User>, UserRepository>();
            builder.Services.AddScoped<IRepository<string, TokenData>, TokenRepository>();
            builder.Services.AddScoped<IRepository<int, Property>, PropertyRepository>();
            builder.Services.AddScoped<IRepository<int, PropertyDetails>, PropertyDetailsRepository>();
            builder.Services.AddScoped<IRepository<int, Media>, MediaRepository>();
            builder.Services.AddScoped<OTPRepository>();
            builder.Services.AddScoped<UserRepository>();
            #endregion

            #region services
            builder.Services.AddScoped<ILoginService, LoginService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<ISmsService, TwilioSmsService>();
            builder.Services.AddScoped<IPropertyService, PropertyService>();
            #endregion

            //#region firebase
            //builder.Services.AddSingleton(FirebaseApp.Create(new AppOptions()
            //{
            //    Credential = GoogleCredential.FromFile("C:\\Users\\VC\\Desktop\\Presidio\\67acres\\RealEstateAPISln\\RealEstateAPI\\serviceAccountKey.json"),
            //}));
            //#endregion


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("MyCors");

            app.MapControllers();

            app.Run();
        }
    }
}
