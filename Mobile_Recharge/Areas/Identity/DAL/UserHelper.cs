using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mobile_Recharge.Areas.Identity.Data;
using Mobile_Recharge.Data;

namespace Mobile_Recharge.Areas.Identity.DAL
{
    public class UserHelper
    {
        private readonly Mobile_RechargeContext _context;
        public UserHelper(Mobile_RechargeContext context) { _context = context; }

        public async Task<List<UserPlanHistory>> GetUserPlans(string id)
        {
            return  _context.userPlanHistory.Include(up=>up.plans).Where(row => row.user.Id == id).ToList();

        }

        public async Task<List<PlansModel>>GetPlans(string userId)
        {
            //get user corresponding to the userId
            Mobile_RechargeUser user=_context.Users.FirstOrDefault(user=>user.Id.Equals(userId));

            //get plans of service provider of user
            return _context.plans.Where(row => row.ServiceProviderId == user.ServiceProviderId).ToList();
        }

        

        
    }
}
