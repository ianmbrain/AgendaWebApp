using AgendaWebApp.Data;
using AgendaWebApp.Data.Enum;
using AgendaWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AgendaWebApp.Service
{
    public class GroupModelRepository : IGroupModelRepository
    {
        private readonly ApplicationDbContext _context;

        public GroupModelRepository(ApplicationDbContext context)
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
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TodoItemModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<TodoItemModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<TodoItemModel> GetByIdAsyncNoTracking(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TodoItemModel>> GetItemByImportance(ImportanceEnum importance)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool Update(TodoItemModel item)
        {
            throw new NotImplementedException();
        }
    }
}
