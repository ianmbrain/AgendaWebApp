using AgendaWebApp.Data;
using AgendaWebApp.Data.Enum;
using AgendaWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AgendaWebApp.Service
{
    public class TodoItemModelRepository : ITodoItemModelRepository
    {
        private readonly ApplicationDbContext _context;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public TodoItemModelRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)  
        {
            // Uses the database tables from the ApplicationDbContext class.
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public bool Add(TodoItemModel item)
        {
            _context.Add(item);
            return Save();
        }

        public bool Delete(TodoItemModel item)
        {
            _context.Remove(item);
            return Save();
        }

        public bool Update(TodoItemModel item)
        {
            _context.Update(item);
            return Save();
        }

        /// <summary>
        /// Returns a list of all the tasks in the database
        /// Task is used to return an object.
        /// ToListAsync is needed from the async Task combo.
        /// </summary>
        /// <returns>List of all the tasks in the database</returns>
        public ICollection<TodoItemModel> GetAll()
        {
            var currentTasks = _context.TodoItems.OrderByDescending(m => m.Importance);
            return currentTasks.ToList();
        }

        public ICollection<TodoItemModel> GetAllByUser()
        {
            var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();

            var currentTasks = (from g in _context.Groups
                                join t in _context.TodoItems on g.GroupId equals t.GroupModelId
                                where g.AppUserId == curUser && t.Finished == false
                                select new TodoItemModel
                                {
                                    Id = t.Id,
                                    Name = t.Name,
                                    Description = t.Description,
                                    Importance = t.Importance,
                                    CreationDate = t.CreationDate,
                                    FinishedDate = t.FinishedDate,
                                    Finished = t.Finished,
                                    GroupModelId = t.GroupModelId
                                }).ToList();
            return currentTasks;
        }

        public TodoItemModel GetById(int id)
        {
            return _context.TodoItems.FirstOrDefault(i => i.Id == id);
        }

        public TodoItemModel GetByIdNoTracking(int id)
        {
            return _context.TodoItems.AsNoTracking().FirstOrDefault(i => i.Id == id);
        }

        public ICollection<TodoItemModel> GetItemByImportance(ImportanceEnum importance)
        {
            return _context.TodoItems.Where(i => i.Importance == importance).ToList();
        }

        public ICollection<TodoItemModel> GetItemByGroupId(int groupId)
        {
            return _context.TodoItems.Where(i => i.GroupModelId == groupId && i.Finished == false).ToList();
        }

        public ICollection<TodoItemModel> GetUnfinishedItems(bool finished)
        {
            return _context.TodoItems.Where(i => i.Finished == finished || i.Finished == null).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public ICollection<int> GetGroups()
        {
            return _context.Groups.Select(i => i.GroupId).ToList();
        }
    }
}
