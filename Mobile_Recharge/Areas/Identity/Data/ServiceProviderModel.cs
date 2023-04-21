using System.ComponentModel.DataAnnotations;

namespace Mobile_Recharge.Areas.Identity.Data
{
    public class ServiceProviderModel
    {
        [Key]
        public int ServiceProviderId { get; set; }

        [Required]
        public string ServiceProviderName { get; set;}
    }
}