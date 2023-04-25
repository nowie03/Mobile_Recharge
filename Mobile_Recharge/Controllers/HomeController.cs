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

        public IActionResult Help()
        {
            return View();
        }

        
        public async Task< IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("admin"))
            {
                return RedirectToAction(actionName:"Index",controllerName:"PlansModels");
            }

                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId != null)
                {

                    var serviceProviderPlans = await _userHelper.GetPlans(userId);
                    UserHomeModel model = new UserHomeModel();
                    var planHistory = await _userHelper.GetUserPlans(userId);
                    model.PlansForUser = serviceProviderPlans;
                if (planHistory.Count != 0)
                {
                    var activePlan = planHistory.Where(row => row.IsActive == true).ToList();
                    model.ActivePlan = activePlan[0];
                    model.PlanHistory = planHistory;
                }
                model.ActivePlan = null;
                model.PlanHistory = null;

                    return View(model);// Code to execute if user is an admin
                }

            return View();

            }


        public  IActionResult Payment(int planId)
        {
            var plan =  _context.plans.Where(plan=>plan.PlanId==planId).ToList();
            ViewData["planId"] = planId;  
            return View(plan);
        }

        public async Task<IActionResult> ProcessPayAsync(int planId)
        {
            //see if there is any current active plan if yes set pending to true
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Mobile_RechargeUser user = _context.Users.Find(userId);
            PlansModel plan = _context.plans.Find(planId);
            if (userId != null )
            {

                string phoneNumber = _context.users.Find(userId).PhoneNumber;

                var planHistory = await _userHelper.GetUserPlans(userId);
                var activePlan = planHistory.Where(row => row.IsActive == true).ToList();

                if (activePlan.Count == 0)
                {
                    //no active plans
                    UserPlanHistory toAddplan = new();
                    toAddplan.StartDate = DateTime.Now;
                    toAddplan.EndDate = DateTime.Now.Add(new TimeSpan(plan.Validity*24, 0, 0));
                    toAddplan.PurchaseDate = DateTime.Now;
                    toAddplan.IsActive = true;
                    toAddplan.IsCompleted = false;
                    toAddplan.IsPending = true;
                    toAddplan.PhoneNumber = phoneNumber;
                    toAddplan.plans = plan;
                    toAddplan.user = user;
                    _context.userPlanHistory.Add(toAddplan);
                    await _context.SaveChangesAsync();

                }
                else
                {
                    //pending plans order by purchase date (recent)
                    List<UserPlanHistory> pendingPlansList=_context.userPlanHistory.Where(row => row.IsPending==true).ToList();

                    if (pendingPlansList.Count == 0)
                    {
                        //no pending plans
                        UserPlanHistory toAddplan = new();
                        toAddplan.StartDate = activePlan[0].EndDate.Add(new TimeSpan(0, 0, 1));
                        toAddplan.EndDate = toAddplan.StartDate.Add(new TimeSpan(plan.Validity * 24, 0, 0));
                        toAddplan.PurchaseDate = DateTime.Now;
                        toAddplan.IsActive = false;
                        toAddplan.IsCompleted = false;
                        toAddplan.IsPending = true;
                        toAddplan.PhoneNumber = phoneNumber;
                        toAddplan.plans = plan;
                        toAddplan.user = user;
                        _context.userPlanHistory.Add(toAddplan);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {

                        UserPlanHistory recentPendingPlan = pendingPlansList[pendingPlansList.Count - 1];

                        UserPlanHistory toAddplan = new();
                        toAddplan.StartDate = recentPendingPlan.EndDate.Add(new TimeSpan(0, 0, 1));
                        toAddplan.EndDate = toAddplan.StartDate.Add(new TimeSpan(plan.Validity * 24, 0, 0));
                        toAddplan.PurchaseDate = DateTime.Now;
                        toAddplan.IsActive = false;
                        toAddplan.IsCompleted = false;
                        toAddplan.IsPending = true;
                        toAddplan.PhoneNumber = phoneNumber;
                        toAddplan.plans = plan;
                        toAddplan.user = user;
                        _context.userPlanHistory.Add(toAddplan);
                        await _context.SaveChangesAsync();
                    }
                }
            }

            return RedirectToAction("Index");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}