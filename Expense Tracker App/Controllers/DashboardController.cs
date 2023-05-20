using Expense_Tracker_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Expense_Tracker_App.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            DateTime startDate = DateTime.Today.AddDays(-6);
            DateTime endDate = DateTime.Today;

            List<Transaction> transactions = await _context.Transactions
                .Include(x => x.Category)
                .Where(x => x.TransactionDate>= startDate && x.TransactionDate <= endDate)
                .ToListAsync();

            int TotalIncome = transactions
                .Where(o => o.Category.type == "Income")
                .Sum(o => o.Amount);
            ViewBag.TotalIncome = TotalIncome.ToString();

            int TotalExpense = transactions
                .Where(o => o.Category.type == "Expense")
                .Sum(o => o.Amount);
            ViewBag.TotalExpense = TotalExpense;

            int Balance = TotalIncome - TotalExpense;
            ViewBag.Balance = Balance;

            return View();
        }
    }
}
