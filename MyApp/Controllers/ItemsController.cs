using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Models;

namespace MyApp.Controllers
{
    public class ItemsController : Controller
    {
        private readonly MyAppDBContext _context;
        public ItemsController(MyAppDBContext context)
        {
            _context = context;
        }

        // i created this function to load the items in the index page
        public async Task<IActionResult> LoadItems()
        {
            var items = await _context.Items.Include(s =>s.SerialNumber).ToListAsync();
            return PartialView("Partials/_ItemListPartial", items);
        }

        // show all items data but use the LoadItems function in the top
        public IActionResult Index()
        {
            return View();
        }

        

        // function that show the create page
        public IActionResult Create()
        {
            return View();
        }

        // function that takes the input of the form create
        [HttpPost]  // used for post,delete and not get action
        public async Task<IActionResult> Create(Item item)
        {
            if (!ModelState.IsValid)
            {
                return View(item);
            }

            try
            {
                _context.Items.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "An error occurred while saving the item. Please try again.");
                return View(item);
            }
        }

        // need to get the item using its id and show the edit page
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _context.Items.FirstOrDefaultAsync(x => x.Id == id);
            return View(item);
        }


        // after the edit in the up, basically this func updated the db 
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, Price")] Item item)
        {
            if (id != item.Id)
            {
                TempData["Error"] = "Invalid item ID.";
                return RedirectToAction(nameof(Index));
            }

            if (!ModelState.IsValid)
            {
                return View(item);
            }

            try
            {
                _context.Update(item);
                await _context.SaveChangesAsync();

                // ✅ Show success message
                TempData["Success"] = "Item updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Items.Any(e => e.Id == item.Id))
                {
                    TempData["Error"] = "This item no longer exists.";
                    return RedirectToAction(nameof(Index));
                }

                TempData["Error"] = "Another user has already updated or deleted this item. Please try again.";
                return RedirectToAction(nameof(Index)); // Or return View(item) if you prefer staying on the page
            }
        }

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
            }

            TempData["Success"] = "Deleted";
            return RedirectToAction(nameof(Index));
        }
    }
}
