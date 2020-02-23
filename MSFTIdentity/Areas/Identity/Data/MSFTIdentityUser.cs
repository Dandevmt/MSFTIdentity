using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MSFTIdentity.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the MSFTIdentityUser class
    public class MSFTIdentityUser : IdentityUser
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
    }
}
