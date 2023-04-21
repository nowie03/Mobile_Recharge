using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Mobile_Recharge.Areas.Identity.Data;

// Add profile data for application users by adding properties to the Mobile_RechargeUser class
public class Mobile_RechargeUser : IdentityUser
{
    


    [Required]
    public int ServiceProviderId { get; set; }

    public ServiceProviderModel serviceProvider { get; set; }
}

