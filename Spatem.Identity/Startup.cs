﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Spatem.Core.Models;
using Spatem.Data.Ef;
using Spatem.Identity.Extensions;
using System.Reflection;

namespace Spatem.Identity
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDataContext(Configuration.GetConnectionString("SpatemConnection"));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddIdentityDataContext()
                .AddDefaultTokenProviders();

            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, AppClaimsPrincipalFactory>();

            services.AddIdentityServer()
                //TODO: add cert generating logic
                .AddDeveloperSigningCredential(true, "tempkey.rsa")
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(Configuration.GetConnectionString("SpatemConnection"),
                            sqlServerOptions => sqlServerOptions.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));
                })
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(Configuration.GetConnectionString("SpatemConnection"),
                            sqlServerOptions => sqlServerOptions.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));
                })
                .AddAspNetIdentity<ApplicationUser>();

            //TODO: add auth to swagger
            services.AddSwaggerDocument(settings =>
            {
                settings.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Spatem Identity API";
                    document.Info.Description = "REST API";
                };
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUi3();
            }
            app.UseIdentityServer();
            app.UseMvcWithDefaultRoute();
        }
    }
}