﻿using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MSFTIdentity.Areas.Identity.Data;
using MSFTIdentity.Data;
using MSFTIdentity.Services;

[assembly: HostingStartup(typeof(MSFTIdentity.Areas.Identity.IdentityHostingStartup))]
namespace MSFTIdentity.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<MSFTIdentityContext>(options =>
                    options.UseMySql(
                        context.Configuration.GetConnectionString("MSFTIdentityContextConnection")));

                services.AddDefaultIdentity<MSFTIdentityUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = true;
                }).AddEntityFrameworkStores<MSFTIdentityContext>();

                services.AddIdentityServer()
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = options => options.UseMySql(context.Configuration.GetConnectionString("MSFTIdentityContextConnection"));
                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = 30;
                })
                .AddInMemoryIdentityResources(ResourceConfig.GetIdentityResources())
                .AddInMemoryApiResources(ResourceConfig.GetApiResources())
                .AddInMemoryClients(ResourceConfig.GetClients())
                .AddAspNetIdentity<MSFTIdentityUser>();

            });
        }
    }
}