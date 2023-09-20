using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Expense_Traker.Data;
using Expense_Traker.Models;
using Microsoft.AspNetCore.Authorization;

namespace Expense_Traker.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TransactionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TransactionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Transaction
        public async Task<IActionResult> index()
        {
            //var applicationDbContext = _context.Transactions.Include(t => t.Category);
            //await applicationDbContext.ToListAsync()
            var application = _context.Transactions.Include(t => t.Category);
            return View(application);
        }



        // GET: Transaction/AddorEdit
        public IActionResult AddorEdit(int id)
        {
            data();
            if (id == 0)
            {

                return View(new Transaction());
            }
            else
            {
                return View(_context.Transactions.Find(id));
            }
            
            
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddorEdit([Bind("TransactionId,CategoryId,Amount,Note,DateTime")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                if(transaction.TransactionId == 0)
                {
                    _context.Add(transaction);
                }
                else
                {
                    _context.Update(transaction);
                }
                
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            data();
            return View(transaction);
        }

        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Transactions == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Transactions'  is null.");
            }
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [NonAction]
        public void data()
        {
            var categories = _context.Categories.ToList();
            Category defualtCategory = new Category { CategoryId = 0, Title = "Choose a category" };
            categories.Insert(0, defualtCategory);
            ViewBag.Categories = categories;    



        }
        
    }
}
