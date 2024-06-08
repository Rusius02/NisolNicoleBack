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
            services.AddSwaggerGen(c => { c.SwaggerDoc("v2", new OpenApiInfo { Title = "My API", Version = "v2" }); c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); });
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
            services.AddSingleton<IWritingEventRepository, WritingEventRepository>();
            //Add usecases
            services.AddSingleton<UseCaseCreateUser>();
            services.AddSingleton<UseCaseDeleteUser>();
            services.AddSingleton<UseCaseListUser>();
            services.AddSingleton<UseCaseCreateBook>();
            services.AddSingleton<UseCaseDeleteBook>();
            services.AddSingleton<UseCaseListBook>();
            services.AddSingleton<UseCaseCreateWritingEvent>();
            services.AddSingleton<UseCaseDeleteWritingEvent>();
            services.AddSingleton<UseCaseListWritingEvent>();
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
            var key = "This is my secret Test key";
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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddSingleton<IJwtAuthentificationManager>(new JwtAuthentificationManager(key));
            StripeConfiguration.ApiKey = "your_stripe_secret_key";
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

            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v2/swagger.json", "My API V2"); });

            app.UseCors(MyOrigins1);

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}