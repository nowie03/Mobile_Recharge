using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mobile_Recharge.Areas.Identity.DAL;
using Mobile_Recharge.Areas.Identity.Data;
using Mobile_Recharge.Data;

namespace Mobile_Recharge.Controllers
{
    public class UserPlanHistoriesController : Controller
    {
        private readonly Mobile_RechargeContext _context;
        private readonly UserHelper _userHelper;
        public UserPlanHistoriesController(Mobile_RechargeContext context)
        {
            _context = context;
            _userHelper = new(context);
        }

        // GET: UserPlanHistories
        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userPlanHistory =await _userHelper.GetUserPlans(userId);

            return View(userPlanHistory);
        }

        // GET: UserPlanHistories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.userPlanHistory == null)
            {
                return NotFound();
            }

            var userPlanHistory = await _context.userPlanHistory
                .FirstOrDefaultAsync(m => m.PlanHistoryId == id);
            if (userPlanHistory == null)
            {
                return NotFound();
            }

            return View(userPlanHistory);
        }

        // GET: UserPlanHistories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserPlanHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlanHistoryId,StartDate,EndDate,PurchaseDate,IsActive,IsCompleted,IsPending,PhoneNumber")] UserPlanHistory userPlanHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userPlanHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userPlanHistory);
        }

        // GET: UserPlanHistories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.userPlanHistory == null)
            {
                return NotFound();
            }

            var userPlanHistory = await _context.userPlanHistory.FindAsync(id);
            if (userPlanHistory == null)
            {
                return NotFound();
            }
            return View(userPlanHistory);
        }

        // POST: UserPlanHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlanHistoryId,StartDate,EndDate,PurchaseDate,IsActive,IsCompleted,IsPending,PhoneNumber")] UserPlanHistory userPlanHistory)
        {
            if (id != userPlanHistory.PlanHistoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userPlanHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserPlanHistoryExists(userPlanHistory.PlanHistoryId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(userPlanHistory);
        }

        // GET: UserPlanHistories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.userPlanHistory == null)
            {
                return NotFound();
            }

            var userPlanHistory = await _context.userPlanHistory
                .FirstOrDefaultAsync(m => m.PlanHistoryId == id);
            if (userPlanHistory == null)
            {
                return NotFound();
            }

            return View(userPlanHistory);
        }

        // POST: UserPlanHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.userPlanHistory == null)
            {
                return Problem("Entity set 'Mobile_RechargeContext.userPlanHistory'  is null.");
            }
            var userPlanHistory = await _context.userPlanHistory.FindAsync(id);
            if (userPlanHistory != null)
            {
                _context.userPlanHistory.Remove(userPlanHistory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserPlanHistoryExists(int id)
        {
          return (_context.userPlanHistory?.Any(e => e.PlanHistoryId == id)).GetValueOrDefault();
        }
    }
}
