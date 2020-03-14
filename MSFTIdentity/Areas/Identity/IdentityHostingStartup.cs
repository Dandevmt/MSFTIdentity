using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MSFTIdentity.Areas.Identity.Data;
using MSFTIdentity.Data;

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


                // Identity Server 4 Setup
                var builder = services.AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                })
                .AddInMemoryIdentityResources(Config.Ids)
                .AddInMemoryApiResources(Config.Apis)
                .AddInMemoryClients(Config.Clients)
                .AddAspNetIdentity<MSFTIdentityUser>();

                // not recommended for production - you need to store your key material somewhere secure
                builder.AddDeveloperSigningCredential();

            });
        }
    }
}