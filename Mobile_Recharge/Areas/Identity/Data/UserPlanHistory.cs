using System.ComponentModel.DataAnnotations;

namespace Mobile_Recharge.Areas.Identity.Data
{
    public class UserPlanHistory
    {
        [Key]
        public int PlanHistoryId { get; set; }

        [Required]

        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        [Required]
        public bool IsPending { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public Mobile_RechargeUser user { get; set; }
        public PlansModel plans { get; set; }
    }
}
