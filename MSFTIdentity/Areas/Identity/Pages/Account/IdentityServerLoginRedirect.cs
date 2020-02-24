using IdentityServer4.Events;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Mvc;
using MSFTIdentity.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSFTIdentity.Areas.Identity.Pages.Account
{
    public class IdentityServerLoginRedirect
    {
        private readonly IIdentityServerInteractionService interaction;
        private readonly IEventService events;

        public IdentityServerLoginRedirect(
            IIdentityServerInteractionService interaction,
            IEventService events)
        {
            this.interaction = interaction;
            this.events = events;
        }

        public async Task<IActionResult> LoginAndRedirect(string returnUrl, MSFTIdentityUser user)
        {
            // check if we are in the context of an authorization request
            var context = await interaction.GetAuthorizationContextAsync(returnUrl);

            await events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id, user.UserName));

            if (context != null)
            {
                return new RedirectResult(returnUrl);
            } else
            {
                return new LocalRedirectResult(returnUrl);
            }

        }
    }
}
