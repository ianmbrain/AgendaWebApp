using AgendaWebApp.Models;
using AgendaWebApp.Service;
using AgendaWebApp.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace AgendaWebApp.Controllers
{
    public class GroupModelController : Controller
    {
        private readonly IGroupModelRepository _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GroupModelController(IGroupModelRepository groupRepo, IHttpContextAccessor httpContextAccessor)
        {
            _context = groupRepo;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<GroupModel> groups = await _context.GetAll();
            return View(groups);
        }

        public async Task<IActionResult> Create()
        {
            // User who is currently logged in. '?' is used to allow current user to be null.
            var curUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var groupViewModel = new CreateGroupViewModel { AppUserId = curUserId };
            return View(groupViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateGroupViewModel groupVM)
        {
            /*if (!ModelState.IsValid)
            {
                return View(group);
            }

            _context.Add(group);
            return RedirectToAction("Index");*/

            if(ModelState.IsValid)
            {
                var group = new GroupModel
                {
                    Name = groupVM.Name,
                    Description = groupVM.Description,
                    AppUserId = groupVM.AppUserId
                };

                _context.Add(group);
                return RedirectToAction("Index");
            }
            else
            {
                return View(groupVM);
            }
        }

        /*public async Task<IActionResult> ViewTasks(int id)
        {

        }*/
    }
}
