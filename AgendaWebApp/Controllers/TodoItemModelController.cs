using AgendaWebApp.Models;
using AgendaWebApp.Service;
using AgendaWebApp.ViewModel;
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

        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View("Views/GroupModel/NoLoginView.cshtml");
            }
            else
            {
                IEnumerable<TodoItemModel> items = _context.GetAllByUser();
                return View(items);
            }
        }

        public IActionResult AllTasks()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View("Views/GroupModel/NoLoginView.cshtml");
            }
            else
            {
                IEnumerable<TodoItemModel> items = _context.GetAll();
                return View(items);
            }
        }

        public IActionResult GetTasksByGroupId(int id)
        {
            IEnumerable<TodoItemModel> items = _context.GetItemByGroupId(id);
            ViewData["currentGroup"] = id;
            return View(items);
        }

        public IActionResult Details(int id)
        {
            TodoItemModel item = _context.GetByIdNoTracking(id);
            return View(item);
        }

        public IActionResult Create(int id)
        {
            // Passes the group id to the card so that users do not have to mannually input a group id
            var itemViewModel = new CreateTodoItemViewModel { GroupModelId = id };
            return View(itemViewModel);
        }

        [HttpPost]
        public IActionResult Create(CreateTodoItemViewModel item)
        {
            if(!ModelState.IsValid)
            {
                return View(item);
            }

            TodoItemModel todo = new TodoItemModel
            {
                Name = item.Name,
                Description = item.Description,
                Importance = item.Importance,
                CreationDate = item.CreationDate,
                FinishedDate = item.FinishedDate,
                Finished = item.Finished,
                GroupModelId = item.GroupModelId
            };
            
            _context.Add(todo);

            return RedirectToAction("GetTasksByGroupId", new { id = item.GroupModelId});
        }

        public IActionResult CreateNoGroup()
        {
            List<int> groups = (List<int>)_context.GetGroups();
            ViewData["GroupList"] = groups;

            var itemViewModel = new CreateTodoItemViewModel { };
            return View(itemViewModel);
        }

        [HttpPost]
        public IActionResult CreateNoGroup(CreateTodoItemViewModel item)
        {
            if (!ModelState.IsValid)
            {
                return View(item);
            }

            TodoItemModel todo = new TodoItemModel
            {
                Name = item.Name,
                Description = item.Description,
                Importance = item.Importance,
                CreationDate = item.CreationDate,
                FinishedDate = item.FinishedDate,
                Finished = item.Finished,
                GroupModelId = item.GroupModelId
            };

            _context.Add(todo);

            return RedirectToAction("GetTasksByGroupId", new { id = item.GroupModelId });
        }

        public IActionResult Edit(int id)
        {
            var item = _context.GetByIdNoTracking(id);

            if(item == null)
            {
                return View("Error");
            }

            var itemVM = new EditItemViewModel
            {
                Id = (int)item.Id,
                Name = item.Name,
                Description = item.Description,
                Importance = item.Importance,
                CreationDate = item.CreationDate,
                FinishedDate = item.FinishedDate,
                Finished = item.Finished,
                GroupModelId = item.GroupModelId
            };

            return View(itemVM);
        }

        [HttpPost]
        public IActionResult Edit(int id, EditItemViewModel itemVM)
        {
            if(!ModelState.IsValid)
            {
                return View("Edit", itemVM);
            }

            var editItem = _context.GetByIdNoTracking(id);

            if(editItem != null)
            {
                var item = new TodoItemModel
                {
                    Id = id,
                    Name = itemVM.Name,
                    Description = itemVM.Description,
                    Importance = itemVM.Importance,
                    CreationDate = itemVM.CreationDate,
                    FinishedDate = itemVM.FinishedDate,
                    Finished = itemVM.Finished,
                    GroupModelId = itemVM.GroupModelId
                };

                _context.Update(item);

                return RedirectToAction("GetTasksByGroupId", new { id = item.GroupModelId });
            }
            else
            {
                return View(itemVM);
            }

        }
        
        public IActionResult Delete(int id)
        {
            var taskDetails = _context.GetById(id);

            if(taskDetails != null) 
            {
                _context.Delete(taskDetails);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public IActionResult Finish(int id)
        {
            var editItem = _context.GetByIdNoTracking(id);

            if (editItem != null)
            {
                var item = new TodoItemModel
                {
                    Id = id,
                    Name = editItem.Name,
                    Description = editItem.Description,
                    Importance = editItem.Importance,
                    CreationDate = editItem.CreationDate,
                    FinishedDate = editItem.FinishedDate,
                    Finished = true,
                    GroupModelId = editItem.GroupModelId
                };

                _context.Update(item);

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }

}
