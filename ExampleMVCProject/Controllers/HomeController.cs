using ExampleMVCProject.Data;
using ExampleMVCProject.Models;
using ExampleMVCProject.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExampleMVCProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db= db;
        }

        public IActionResult Index()
        {
            var chartSumary = new ChartDataSumaryVM();

            chartSumary.TotalExpensesNotPaid = _db.Expenses.Count(e => e.PaidDate == null);
            chartSumary.TotalExpensesPaid = _db.Expenses.Count(e => e.PaidDate != null);
            chartSumary.TotalItemsBorrowedReturned = _db.Items.Count(i => i.ReturnedDate != null);
            chartSumary.TotalItemsBorrowedNotReturned = _db.Items.Count(i => i.ReturnedDate == null);
            return View(chartSumary);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}