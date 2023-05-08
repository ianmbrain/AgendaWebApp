using AgendaWebApp.Models;
using AgendaWebApp.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgendaWebApp.Controllers
{
    public class GroupModelController : Controller
    {
        private readonly GroupModelRepository _context;

        public GroupModelController(GroupModelRepository GroupRepo)
        {
            _context = GroupRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TodoItemModel item)
        {
            if (!ModelState.IsValid)
            {
                return View(item);
            }

            _context.Add(item);
            return RedirectToAction("Index");
        }
    }
}
