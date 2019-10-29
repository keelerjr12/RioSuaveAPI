using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RioSuaveLib;
using RioSuaveLib.Events;
using RioSuaveLib.JWT;

namespace RioSuaveAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SigningKey"]));
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins, builder =>
                {
                    //builder.WithOrigins("https://rio-suave.appspot.com").AllowAnyHeader();
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ClockSkew = TimeSpan.FromMinutes(5),
                        IssuerSigningKey = _signingKey,
                        RequireSignedTokens = true,
                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                        ValidateAudience = true,
                        ValidAudience = Configuration["JWT:Audience"],
                        ValidateIssuer = true,
                        ValidIssuer = Configuration["JWT:Issuer"]
                    };
                });

            services.AddDbContext<RioSuaveContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("rioSuaveDbConnStr")));
            services.AddScoped<AuthService, AuthService>();
            services.AddScoped(s => new JwtService(Configuration["JWT:Issuer"], Configuration["JWT:Audience"], _signingKey));
            services.AddScoped(s => new EmailService(Configuration["Host"], int.Parse(Configuration["Port"]), Configuration["Username"], Configuration["Password"]));
            services.AddScoped<EventsService, EventsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        private readonly SymmetricSecurityKey _signingKey;
    }
}
