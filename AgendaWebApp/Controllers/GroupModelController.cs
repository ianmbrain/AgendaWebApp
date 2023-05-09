using AgendaWebApp.Models;
using AgendaWebApp.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgendaWebApp.Controllers
{
    public class GroupModelController : Controller
    {
        private readonly IGroupModelRepository _context;

        public GroupModelController(IGroupModelRepository groupRepo)
        {
            _context = groupRepo;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<GroupModel> groups = await _context.GetAll();
            return View(groups);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(GroupModel group)
        {
            if (!ModelState.IsValid)
            {
                return View(group);
            }

            _context.Add(group);
            return RedirectToAction("Index");
        }


    }
}
