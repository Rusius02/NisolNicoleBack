using Infrastructure.SqlServer.Repository.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using NisolNicole.Utils;
using Microsoft.OpenApi.Models;
using Infrastructure.SqlServer.System;
using Application.UseCases.Books;
using Infrastructure.SqlServer.Repository.Books;
using Stripe;
using Application.UseCases.WritingEvents;
using Infrastructure.SqlServer.Repository.WritingEvents;
using Application.UseCases.NewFolder;
using Infrastructure.Services.Contact;
using NisolNicole.security.proxy;
using Infrastructure.SqlServer.Repository.Orders;
using Application.UseCases.Orders;
using Application.UseCases.SiteTraffic;
using Infrastructure.SqlServer.Repository.SiteTraffic;
using Application.UseCases.Shipping;
using Infrastructure.SqlServer.Repository.ShippingInfos;

namespace NisolNicole
{
    public class Startup
    {
        public static readonly string MyOrigins1 = "MyOrigins1";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c => 
            {
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "My API", Version = "v2" }); c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.MapType<IFormFile>(() => new OpenApiSchema { Type = "string", Format = "binary" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Saisis ici ton token JWT : Bearer {ton_token}"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        new string[] {}
                    }
                });
            });
            services.AddCors(options =>
            {
                options.AddPolicy(MyOrigins1, builder =>
                {
                    builder.WithOrigins("http://localhost:8080")
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });

            });
            services.AddSingleton<IDatabaseManager, DatabaseManager>();
            //Add repos
            services.AddSingleton<IUsersRepository, UsersRepository>();
            services.AddSingleton<IBookRepository, BookRepository>();
            services.AddSingleton<IOrderRepository, OrderRepository>();
            services.AddSingleton<ISiteTrafficRepository, SiteTrafficRepository>();
            services.AddSingleton<IWritingEventRepository, WritingEventRepository>();
            services.AddSingleton<ShippingInfosIRepository, ShippingInfosRepository>();
            //Add usecases
            services.AddSingleton<UseCaseCreateUser>();
            services.AddSingleton<UseCaseDeleteUser>();
            services.AddSingleton<UseCaseListUser>();
            services.AddSingleton<UseCaseCreateBook>();
            services.AddSingleton<UseCaseDeleteBook>();
            services.AddSingleton<UseCaseListBook>();
            services.AddSingleton<UseCaseUpdateStock>();
            services.AddSingleton<UseCaseGetStock>();
            services.AddSingleton<UsecaseCreateOrder>();
            services.AddSingleton<UseCaseCreateWritingEvent>();
            services.AddSingleton<UseCaseDeleteWritingEvent>();
            services.AddSingleton<UseCaseListWritingEvent>();
            services.AddSingleton<SiteTrafficService>();
            services.AddSingleton<UsecaseCreateShippingInfos>();
            //SMTP config
            var emailSettings = Configuration.GetSection("EmailSettings").Get<EmailSettingProxy>();
            services.AddSingleton<IEmailService>(new EmailService(
                   smtpServer: emailSettings.SmtpServer,
                   smtpPort: emailSettings.SmtpPort,
                   smtpUser: emailSettings.SmtpUser,
                   smtpPass: emailSettings.SmtpPass
               ));

            services.AddSingleton<UseCaseContactAuthorByMail>();
            //Authentication
            var jwtSettings = Configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings.GetValue<string>("SecretKey");
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddSingleton<IJwtAuthentificationManager>(new JwtAuthentificationManager(secretKey, Configuration));
            var stripe = Configuration.GetSection("Stripe");
            var secretStripeKey = stripe.GetValue<string>("SecretKey");
            StripeConfiguration.ApiKey = secretStripeKey;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHttpsRedirection();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c => 
            {
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "API de gestion de livres v2");
            });

            app.UseCors(MyOrigins1);

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}