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

        public bool Add(GroupModel item)
        {
            _context.Add(item);
            return Save();
        }

        public bool Delete(GroupModel item)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GroupModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<GroupModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GroupModel> GetByIdAsyncNoTracking(int id)
        {
            throw new NotImplementedException();
        }


        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(GroupModel item)
        {
            throw new NotImplementedException();
        }
    }
}
