using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mobile_Recharge.Areas.Identity.Data;

namespace Mobile_Recharge.Data;

public class Mobile_RechargeContext : IdentityDbContext<Mobile_RechargeUser>
{
    public Mobile_RechargeContext(DbContextOptions<Mobile_RechargeContext> options)
        : base(options)
    {
    }
    public DbSet<Mobile_RechargeUser>users{ get; set; }
    public DbSet<ServiceProviderModel> serviceProviders { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

    }
}
