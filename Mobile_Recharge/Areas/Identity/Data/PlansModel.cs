using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Mobile_Recharge.Areas.Identity.Data
{
    public class PlansModel
    {
        [Key]
        public int PlanId { get; set; }

        [Required]
        public string PlanName { get; set; }

        [Required]
        public string PlanDescription { get; set; }

        [AllowNull]
        public int Validity { get; set; }

        [AllowNull]
        public int Data { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public bool IsRoaming { get; set; }

        [Required]
        public int ServiceProviderId { get; set; }

        public ServiceProviderModel serviceProvider { get; set; }


    
    }
}
