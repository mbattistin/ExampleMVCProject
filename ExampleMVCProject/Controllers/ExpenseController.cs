using ExampleMVCProject.Data;
using ExampleMVCProject.Models;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace ExampleMVCProject.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IToastNotification _toastNotification;
        public ExpenseController(ApplicationDbContext db, IToastNotification toastNotification)
        {
            _db = db;
            _toastNotification = toastNotification;
        }

        public IActionResult Index(string sortOrder, string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            ViewData["NameSortParm"] = sortOrder == "name" ? "name_desc" : "name";
            ViewData["PaidDateSortParm"] = sortOrder == "paidDate" ? "paidDate_desc" : "paidDate";
            ViewData["AmountSortParm"] = sortOrder == "amount" ? "amount_desc" : "amount";

            ViewData["CurrentSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name" : sortOrder;
            try
            {
                IEnumerable<Expense> expenses;
                if (string.IsNullOrEmpty(searchString))
                {
                    expenses = _db.Expenses;
                }
                else
                {
                    expenses = _db.Expenses.Where(e => e.Name.Contains(searchString));
                }

                switch (sortOrder)
                {
                    case "name_desc":
                        expenses = expenses.OrderByDescending(e => e.Name);
                        break;
                    case "paidDate":
                        expenses = expenses.OrderBy(e => e.PaidDate);
                        break;
                    case "paidDate_desc":
                        expenses = expenses.OrderByDescending(e => e.PaidDate);
                        break;
                    case "amount":
                        expenses = expenses.OrderBy(e => e.Amount);
                        break;
                    case "amount_desc":
                        expenses = expenses.OrderByDescending(e => e.Amount);
                        break;
                    default:
                        expenses = expenses.OrderBy(e => e.Name);
                        break;
                }
                return View(expenses);

            }
            catch
            {
                _toastNotification.AddErrorToastMessage("Failed to get expenses!");
                return View();
            }

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Expense expense)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Expenses.Add(expense);
                    _db.SaveChanges();
                    _toastNotification.AddSuccessToastMessage("Expense saved!");
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                _toastNotification.AddErrorToastMessage("Failed to create expense!");
            }
            return View(expense);

        }

        public IActionResult Update(int id)
        {

            var expense = _db.Expenses.Find(id);
            if (expense == null)
            {
                _toastNotification.AddErrorToastMessage("Failed to update expense!");
                return NotFound();
            }
            return View(expense);

        }

        [HttpPost]
        public IActionResult Update(Expense expense)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    _db.Expenses.Update(expense);
                    _db.SaveChanges();
                    _toastNotification.AddSuccessToastMessage("Expense updated!");
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                _toastNotification.AddErrorToastMessage("Failed to update expense!");
            }
            return View(expense);

        }
        public IActionResult Delete(int id)
        {
            try
            {
                var expense = _db.Expenses.Find(id);
                if (expense == null)
                {
                    _toastNotification.AddErrorToastMessage("Failed to delete expense!");
                    return RedirectToAction("Index");
                }

                _db.Expenses.Remove(expense);
                _db.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Expense deleted!");
            }
            catch
            {
                _toastNotification.AddErrorToastMessage("Failed to delete expense!");
            }

            return RedirectToAction("Index");
        }
    }
}
