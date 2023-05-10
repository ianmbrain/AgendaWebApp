using AgendaWebApp.Data;
using AgendaWebApp.Models;
using AgendaWebApp.Service;
using AgendaWebApp.ViewModel;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> Index()
        {
            // This is returning the list of the items in the TodoItems table.
            IEnumerable<TodoItemModel> items = await _context.GetAll();
            return View(items);
        }

        public async Task<IActionResult> GetTasksByGroupId(int id = 0)
        {
            IEnumerable<TodoItemModel> items = await _context.GetItemByGroupId(id);
            ViewData["currentGroup"] = id;
            return View(items);
        }

        public async Task<IActionResult> Details(int id)
        {
            // Include "Include(a => a.GroupModelId)." after TodoItems if using join
            TodoItemModel item = await _context.GetByIdAsync(id);
            return View(item);
        }

        public async Task<IActionResult> Create(int id)
        {
            TodoItemModel item = new TodoItemModel { Name = "", Description = "", CreationDate = DateTime.Now, FinishedDate =  DateTime.Now, Finished = false, Importance = 0, GroupModelId = id}; ;
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TodoItemModel item)
        {
            // If the input is not valid the view will remain with validation text
            if(!ModelState.IsValid)
            {
                return View(item);
            }

            _context.Add(item);
            return RedirectToAction("Index");
        }

       /* public async Task<IActionResult> CreateByGroupId(int id)
        {
            return View(id);
        }

        [HttpPost]
        public async Task<IActionResult> CreateByGroupId(TodoItemModel item)
        {
            // If the input is not valid the view will remain with validation text
            if (!ModelState.IsValid)
            {
                return View(item);
            }

            _context.Add(item);
            return RedirectToAction("Index");
        }*/

        /// <summary>
        /// Returns the edit view preloaded with the values from the specified item.
        /// Uses the item repository service to retrieve the item from the database
        /// </summary>
        /// <param name="id"> Id of item to edit </param>
        /// <returns> View containing the item values preloaded </returns>
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _context.GetByIdAsync(id);

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
        public async Task<IActionResult> Edit(int id, EditItemViewModel itemVM)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Unable to edit item");
                return View("Edit", itemVM);
            }

            var editItem = await _context.GetByIdAsyncNoTracking(id);

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

                return RedirectToAction("Index");
            }
            else
            {
                return View(itemVM);
            }

        }
        
        public async Task<IActionResult> Delete(int id)
        {
            var taskDetails = await _context.GetByIdAsync(id);

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

        public async Task<IActionResult> Finish(int id)
        {
            var editItem = await _context.GetByIdAsyncNoTracking(id);

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
