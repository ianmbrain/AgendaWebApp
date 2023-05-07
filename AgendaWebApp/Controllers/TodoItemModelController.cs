using AgendaWebApp.Data;
using AgendaWebApp.Models;
using AgendaWebApp.Service;
using Microsoft.AspNetCore.Mvc;

namespace AgendaWebApp.Controllers
{
    public class TodoItemModelController : Controller
    {
        private readonly ITodoItemModelRepository _context;

        public TodoItemModelController(ITodoItemModelRepository todoRepo) 
        {
            _context = todoRepo;
        }

        public async Task<IActionResult> Index()
        {
            // This is returning the list of the items in the TodoItems table.
            IEnumerable<TodoItemModel> items = await _context.GetAll();
            return View(items);
        }

        public async Task<IActionResult> Details(int id)
        {
            // Include "Include(a => a.GroupModelId)." after TodoItems if using join
            TodoItemModel item = await _context.GetByIdAsync(id);
            return View(item);
        }

        public async Task<IActionResult> Create()
        {
            return View(); 
        }
    }
}
