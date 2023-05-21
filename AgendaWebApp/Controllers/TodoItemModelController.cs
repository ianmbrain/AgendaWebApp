using AgendaWebApp.Data;
using AgendaWebApp.Models;
using AgendaWebApp.Service;
using AgendaWebApp.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;

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

        // Defaults to 0 so that if no group id is provided it uses the "0" group id which no group has
        public IActionResult GetTasksByGroupId(int id = 0)
        {
            IEnumerable<TodoItemModel> items = _context.GetItemByGroupId(id);
            ViewData["currentGroup"] = id;
            return View(items);
        }

        public IActionResult Details(int id)
        {
            // Include "Include(a => a.GroupModelId)." after TodoItems if using join
            TodoItemModel item = _context.GetById(id);
            return View(item);
        }

        public IActionResult Create(int id)
        {
            // Used to pass in the groupId. Needed as otherwise the program assumes the id is id of the card.
            // This can be redone using a view model like done for passing the current user to group
            //TodoItemModel item = new TodoItemModel { Name = "", Description = "", CreationDate = DateTime.Now, FinishedDate =  DateTime.Now, Finished = false, Importance = 0, GroupModelId = id}; ;
            var itemViewModel = new CreateTodoItemViewModel { GroupModelId = id };
            return View(itemViewModel);
        }

        [HttpPost]
        public IActionResult Create(CreateTodoItemViewModel item)
        {
            // If the input is not valid the view will remain with validation text
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
            //SelectList groupList = new SelectList(groups, "Id");
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

        /// <summary>
        /// Returns the edit view preloaded with the values from the specified item.
        /// Uses the item repository service to retrieve the item from the database
        /// </summary>
        /// <param name="id"> Id of item to edit </param>
        /// <returns> View containing the item values preloaded </returns>
        public IActionResult Edit(int id)
        {
            var item = _context.GetById(id);

            if(item == null)
            {
                return View("Error");
            }

            // Creates a new itemVM using the values from the item. Inserts them into the view to preload the values.
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

            // Returns the view preloaded with the item fields.
            return View(itemVM);
        }

        /// <summary>
        /// Edit post action. Utilizes the itemVM object for the edit get action. 
        /// Substitutes the value of the item with the associated id with update values from the item view model.
        /// </summary>
        /// <param name="id"> Id of task to update </param>
        /// <param name="itemVM"> Task that was created from the get edit view </param>
        /// <returns> The index view if the update was successful or edit view if a value was incorrect </returns>
        [HttpPost]
        public IActionResult Edit(int id, EditItemViewModel itemVM)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Unable to edit item");
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
