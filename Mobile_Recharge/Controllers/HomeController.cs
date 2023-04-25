using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mobile_Recharge.Areas.Identity.DAL;
using Mobile_Recharge.Areas.Identity.Data;
using Mobile_Recharge.Data;
using Mobile_Recharge.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace Mobile_Recharge.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Mobile_RechargeContext _context;

        private readonly UserHelper _userHelper;

        public HomeController(ILogger<HomeController> logger, Mobile_RechargeContext context)
        {
            _userHelper = new(context);
            _logger = logger;
            _context = context;
        }

        
        public async Task< IActionResult> Index()
        {
           
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                var serviceProviderPlans = await _userHelper.GetPlans(userId);
                UserHomeModel model = new UserHomeModel();
                var activePlan = await _userHelper.GetUserPlans(userId);
                activePlan = activePlan.Where(row => row.IsActive == true).ToList();
                model.PlansForUser = serviceProviderPlans;
                model.ActivePlan = activePlan[0];

                return View(model);
            }
            return View();
            
        }

        public IActionResult Payment(int planId)
        {
            //see if there is any current active plan if yes set pending to true
            ViewData["planId"] = planId;  
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}