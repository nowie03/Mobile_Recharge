namespace Mobile_Recharge.Areas.Identity.Data
{
    public class UserHomeModel
    {
        public List<PlansModel> PlansForUser { get; set; }

        public UserPlanHistory ActivePlan { get; set; }

        public List<UserPlanHistory> PlanHistory { get; set; }
    }
    
}
