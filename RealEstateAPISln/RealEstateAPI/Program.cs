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
using Azure.Security.KeyVault.Secrets;
using Azure.Identity;
using Azure.Storage.Blobs;
using System.Diagnostics.CodeAnalysis;
using Azure.Communication.Email;


namespace RealEstateAPI
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        private static async Task<string> GetSqlCs()
        {
            const string secretName = "mv-67acres-sql-cs";
            var keyVaultName = "mv-67acres-cs";
            var kvUri = $"https://{keyVaultName}.vault.azure.net";
            var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());
            var secret = await client.GetSecretAsync(secretName);
            return secret.Value.Value;
        }

        private static async Task<string> GetEmailCs()
        {
            const string secretName = "mv-67acres-email-cs";
            var keyVaultName = "mv-67acres-cs";
            var kvUri = $"https://{keyVaultName}.vault.azure.net";
            var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());
            var secret = await client.GetSecretAsync(secretName);
            return secret.Value.Value;
        }


        private static async Task<string> GetBlobCs()
        {
            const string secretName = "mv-67acres-blob-cs";
            var keyVaultName = "mv-67acres-cs";
            var kvUri = $"https://{keyVaultName}.vault.azure.net";
            var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());
            var secret = await client.GetSecretAsync(secretName);
            return secret.Value.Value;
        }

        private static async Task<string> GetTwilioSid()
        {
            const string secretName = "mv-67acres-twilio-sid";
            var keyVaultName = "mv-67acres-cs";
            var kvUri = $"https://{keyVaultName}.vault.azure.net";
            var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());
            var secret = await client.GetSecretAsync(secretName);
            return secret.Value.Value;
        }

        private static async Task<string> GetTwilioToken()
        {
            const string secretName = "mv-67acres-twilio-token";
            var keyVaultName = "mv-67acres-cs";
            var kvUri = $"https://{keyVaultName}.vault.azure.net";
            var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());
            var secret = await client.GetSecretAsync(secretName);
            return secret.Value.Value;
        }

        public static async Task Main(string[] args)
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
                    options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().WithHeaders("Authorization"); ;
                });
            });
            #endregion

            #region context
            //builder.Services.AddDbContext<RealEstateAppContext>(
            //   options => options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"))
            //   );

            var defaultConnection = await GetSqlCs();

            builder.Services.AddDbContext<RealEstateAppContext>(
                options => options.UseSqlServer(defaultConnection)
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
            builder.Services.AddScoped<TokenRepository>();
            #endregion

            #region services
            builder.Services.AddScoped<ILoginService, LoginService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            //builder.Services.AddScoped<ISmsService, TwilioSmsService>();
            builder.Services.AddScoped<IPropertyService, PropertyService>();
            builder.Services.AddScoped<IBlobService, BlobStorageService>();
            builder.Services.AddScoped< EmailService>();

            var blobcs = await GetBlobCs();
            builder.Services.AddSingleton(x => new BlobServiceClient(blobcs));

            var email = await GetEmailCs();
            //EmailClient emailClient = new EmailClient(email);
            builder.Services.AddSingleton(x => new EmailClient(email));

            var twilioAccountSid = await GetTwilioSid();
            var twilioAuthToken = await GetTwilioToken();
            builder.Services.AddSingleton<ISmsService>(new TwilioSmsService(twilioAccountSid, twilioAuthToken));
            #endregion




            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseCors("MyCors");
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
