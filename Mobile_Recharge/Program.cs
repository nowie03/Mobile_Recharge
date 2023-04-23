using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mobile_Recharge.Areas.Identity.DAL;
using Mobile_Recharge.Areas.Identity.Data;
using Mobile_Recharge.Data;
namespace Mobile_Recharge
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
                        var connectionString = builder.Configuration.GetConnectionString("Mobile_RechargeContextConnection") ?? throw new InvalidOperationException("Connection string 'Mobile_RechargeContextConnection' not found.");

                                    builder.Services.AddDbContext<Mobile_RechargeContext>(options =>
                options.UseSqlServer(connectionString));

                                                builder.Services.AddDefaultIdentity<Mobile_RechargeUser>(options => options.SignIn.RequireConfirmedAccount = false).AddRoles<IdentityRole>().
                AddEntityFrameworkStores<Mobile_RechargeContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<UserHelper, UserHelper>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
                        app.UseAuthentication();;

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages(); 

            app.Run();
        }
    }
}