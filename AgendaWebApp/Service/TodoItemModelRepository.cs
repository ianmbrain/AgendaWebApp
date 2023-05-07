using AgendaWebApp.Data;
using AgendaWebApp.Data.Enum;
using AgendaWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AgendaWebApp.Service
{
    public class TodoItemModelRepository : ITodoItemModelRepository
    {
        private readonly ApplicationDbContext _context;

        public TodoItemModelRepository(ApplicationDbContext context)  
        { 
            // Uses the database tables from the ApplicationDbContext class.
            _context = context;
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

        /// <summary>
        /// Returns a list of all the tasks in the database
        /// Task is used to return an object.
        /// ToListAsync is needed from the async Task combo.
        /// </summary>
        /// <returns>List of all the tasks in the database</returns>
        public async Task<IEnumerable<TodoItemModel>> GetAll()
        {
            return await _context.TodoItems.ToListAsync();
        }

        public async Task<TodoItemModel> GetByIdAsync(int id)
        {
            return await _context.TodoItems.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<TodoItemModel>> GetItemByImportance(ImportanceEnum importance)
        {
            return await _context.TodoItems.Where(i => i.Importance == importance).ToListAsync();
        }

        public async Task<IEnumerable<TodoItemModel>> GetUnfinishedItems(bool finished)
        {
            return await _context.TodoItems.Where(i => i.Finished == finished || i.Finished == null).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
