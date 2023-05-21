using AgendaWebApp.Data;
using AgendaWebApp.Data.Enum;
using AgendaWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AgendaWebApp.Service
{
    public class GroupModelRepository : IGroupModelRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GroupModelRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            // Uses the database tables from the ApplicationDbContext class.
            _context = context;
            _httpContextAccessor = httpContextAccessor;
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

        public ICollection<GroupModel> GetAll()
        {
            var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userGroups = _context.Groups.Where(i => i.AppUserId == curUser).OrderBy(x => x.GroupId);
            return userGroups.ToList();
        }

        public GroupModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public GroupModel GetByIdNoTracking(int id)
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
