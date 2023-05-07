using AgendaWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgendaWebApp.Controllers
{
    public class TodoItemModelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
