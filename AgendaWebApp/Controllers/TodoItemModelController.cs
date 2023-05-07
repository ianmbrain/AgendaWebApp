using AgendaWebApp.Data;
using AgendaWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgendaWebApp.Controllers
{
    public class TodoItemModelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TodoItemModelController(ApplicationDbContext context) 
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // This is returning the list of the items in the TodoItems table.
            var items = _context.TodoItems.ToList();
            return View(items);
        }
    }
}
