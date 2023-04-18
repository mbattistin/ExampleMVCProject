using ExampleMVCProject.Data;
using ExampleMVCProject.Models;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace ExampleMVCProject.Controllers
{
    public class ItemController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IToastNotification _toastNotification;

        public ItemController(ApplicationDbContext db, IToastNotification toastNotification)
        {
            _db = db;
            _toastNotification = toastNotification;

        }

        public IActionResult Index(string sortOrder, string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            ViewData["NameSortParm"] = sortOrder == "name" ? "name_desc" : "name";
            ViewData["BorrowerSortParm"] = sortOrder == "borrower" ? "borrower_desc" : "borrower";
            ViewData["LenderSortParm"] = sortOrder == "lender" ? "lender_desc" : "lender";
            ViewData["ReturnedDateSortParm"] = sortOrder == "returnedDate" ? "returnedDate_desc" : "returnedDate";

            ViewData["CurrentSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name" : sortOrder;

            try
            {
                IEnumerable<Item> items = _db.Items;
                if (string.IsNullOrEmpty(searchString))
                {
                    items = _db.Items;
                }
                else
                {
                    items = _db.Items.Where(e => e.Name.Contains(searchString) ||
                    e.Borrower.Contains(searchString) ||
                    e.Lender.Contains(searchString));
                }

                switch (sortOrder)
                {
                    case "name_desc":
                        items = items.OrderByDescending(e => e.Name);
                        break;
                    case "borrower":
                        items = items.OrderBy(e => e.Borrower);
                        break;
                    case "borrower_desc":
                        items = items.OrderByDescending(e => e.Borrower);
                        break;
                    case "lender":
                        items = items.OrderBy(e => e.Lender);
                        break;
                    case "lender_desc":
                        items = items.OrderByDescending(e => e.Lender);
                        break;
                    case "returnedDate":
                        items = items.OrderBy(e => e.ReturnedDate);
                        break;
                    case "returnedDate_desc":
                        items = items.OrderByDescending(e => e.ReturnedDate);
                        break;
                    default:
                        items = items.OrderBy(e => e.Name);
                        break;
                }

                return View(items);
            }
            catch
            {
                _toastNotification.AddErrorToastMessage("Failed to get borrowed items!");
                return View();
            }

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Item item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Items.Add(item);
                    _db.SaveChanges();
                    _toastNotification.AddSuccessToastMessage("Borrowed item saved!");
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                _toastNotification.AddErrorToastMessage("Failed to create borrowed item!");
            }
            return View(item);

        }

        public IActionResult Update(int id)
        {

            var item = _db.Items.Find(id);
            if (item == null)
            {
                _toastNotification.AddErrorToastMessage("Failed to update borrowed item!");
                return NotFound();
            }
            return View(item);

        }

        [HttpPost]
        public IActionResult Update(Item item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Items.Update(item);
                    _db.SaveChanges();
                    _toastNotification.AddSuccessToastMessage("Borrowed item updated!");
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                _toastNotification.AddErrorToastMessage("Failed to update borrowed item!");
            }

            return View(item);

        }


        public IActionResult Delete(int id)
        {
            try
            {
                var item = _db.Items.Find(id);
                if (item == null)
                {
                    _toastNotification.AddErrorToastMessage("Failed to delete borrowed item!");
                    return NotFound();
                }

                _db.Items.Remove(item);
                _db.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Borrowed item deleted!");
            }
            catch
            {
                _toastNotification.AddErrorToastMessage("Failed to delete borrowed item!");

            }


            return RedirectToAction("Index");

        }
    }
}
