﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
            return  _context.userPlanHistory.Where(row=>row.user.Id==id).ToList();
       
        }

        public async Task<List<PlansModel>>GetPlans(int serviceProviderId)
        {
            return _context.plans.Where(row => row.ServiceProviderId == serviceProviderId).ToList();
        }
    }
}
