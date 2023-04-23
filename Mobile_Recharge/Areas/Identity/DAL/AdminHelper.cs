using Mobile_Recharge.Areas.Identity.Data;
using Mobile_Recharge.Data;

namespace Mobile_Recharge.Areas.Identity.DAL
{
    public class AdminHelper
    {
        private readonly Mobile_RechargeContext _context;

        public AdminHelper(Mobile_RechargeContext context)
        {
            _context = context;
        }

        public  List<Mobile_RechargeUser> GetUsers()
        {
            return _context.users.ToList();
        }

        public List<ServiceProviderModel> GetServiceProviders()
        {
            return _context.serviceProviders.ToList();
        }

        public List<PlansModel> GetPlans()
        {
            return _context.plans.ToList();
        }



        public List<UserPlanHistory> GetUserPlanHistory()
        {
            return _context.userPlanHistory.ToList();   
        }

        public List<UserPlanHistory> GetUserPlanHistory(string id)
        {
            
            return _context.userPlanHistory.Where(plan=>plan.user.Id==id).ToList();
        }

        
        public PlansModel AddPlan(PlansModel plan)
        {
            _context.plans.Add(plan);
            _context.SaveChanges();
            return plan;
        }

        public PlansModel DeletePlan(PlansModel plan)
        {
            _context.plans.Remove(plan);
            _context.SaveChanges();
            return plan;
        }

        public PlansModel UpdatePlan(PlansModel plan) {

            _context.plans.Update(plan);

            _context.SaveChanges();

            return plan;

            
        }






    }
}
