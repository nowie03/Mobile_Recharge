using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mobile_Recharge.Areas.Identity.Data;
using Mobile_Recharge.Data;

namespace Mobile_Recharge.Controllers
{
    [Authorize(Roles="admin")]
    public class PlansModelsController : Controller
    {
        private readonly Mobile_RechargeContext _context;

        public PlansModelsController(Mobile_RechargeContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> ViewUserPlanHistory()
        {
            var mobile_RechargeContext = _context.userPlanHistory.Include(p => p.plans).Include(p => p.user).Include(p=>p.plans.serviceProvider);
            return View(await mobile_RechargeContext.ToListAsync());
        }

        // GET: PlansModels
        public async Task<IActionResult> Index()
        {
            var mobile_RechargeContext = _context.plans.Include(p => p.serviceProvider);
            return View(await mobile_RechargeContext.ToListAsync());
        }

        // GET: PlansModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.plans == null)
            {
                return NotFound();
            }

            var plansModel = await _context.plans
                .Include(p => p.serviceProvider)
                .FirstOrDefaultAsync(m => m.PlanId == id);
            if (plansModel == null)
            {
                return NotFound();
            }

            return View(plansModel);
        }

        // GET: PlansModels/Create
        public IActionResult Create()
        {
            ViewData["ServiceProviderId"] = new SelectList(_context.serviceProviders, "ServiceProviderId", "ServiceProviderName");
            return View();
        }

        // POST: PlansModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("planId,PlanName,PlanDescription,Validity,Data,Price,IsRoaming,ServiceProviderId")] PlansModel plansModel)
        {
            try
            {
                Console.WriteLine("IN post valid " + plansModel.PlanName);
                _context.Add(plansModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex) {
                Console.WriteLine(ex.Message);
            }
            

            ViewData["ServiceProviderId"] = new SelectList(_context.serviceProviders, "ServiceProviderId", "ServiceProviderName", plansModel.ServiceProviderId);
            return View(plansModel);
        }

        // GET: PlansModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.plans == null)
            {
                return NotFound();
            }

            var plansModel = await _context.plans.FindAsync(id);
            if (plansModel == null)
            {
                return NotFound();
            }
            ViewData["ServiceProviderId"] = new SelectList(_context.serviceProviders, "ServiceProviderId", "ServiceProviderName", plansModel.ServiceProviderId);
            return View(plansModel);
        }

        // POST: PlansModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlanId,PlanName,PlanDescription,Validity,Data,Price,IsRoaming,ServiceProviderId")] PlansModel plansModel)
        {
            if (id != plansModel.PlanId)
            {
                return NotFound();
            }

           
            try
                {
                    _context.Update(plansModel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                Console.WriteLine("DB CONCURRENCY EXCEPTION");
                }
            
            ViewData["ServiceProviderId"] = new SelectList(_context.serviceProviders, "ServiceProviderId", "ServiceProviderName", plansModel.ServiceProviderId);
            return View(plansModel);
        }

        // GET: PlansModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.plans == null)
            {
                return NotFound();
            }

            var plansModel = await _context.plans
                .Include(p => p.serviceProvider)
                .FirstOrDefaultAsync(m => m.PlanId == id);
            if (plansModel == null)
            {
                return NotFound();
            }

            return View(plansModel);
        }

        // POST: PlansModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.plans == null)
            {
                return Problem("Entity set 'Mobile_RechargeContext.plans'  is null.");
            }
            var plansModel = await _context.plans.FindAsync(id);
            if (plansModel != null)
            {
                _context.plans.Remove(plansModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlansModelExists(int id)
        {
          return (_context.plans?.Any(e => e.PlanId == id)).GetValueOrDefault();
        }
    }
}
