using AgendaWebApp.Data;
using AgendaWebApp.Models;
using Microsoft.AspNetCore.Identity;

namespace AgendaWebApp.Service
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public ICollection<IdentityUser> GetAll()
        {
            return _context.Users.ToList();
        }

        public int GetActiveTaskCount(string userId)
        {
            var activeTasks = (from g in _context.Groups
                               join u in _context.Users on g.AppUserId equals u.Id
                               join t in _context.TodoItems on g.GroupId equals t.GroupModelId
                               where u.Id == userId
                               where t.Finished == false
                               select t.Id).Count();

            return activeTasks;
        }

        public int GetFinishedTaskCount(string userId)
        {
            var activeTasks = (from g in _context.Groups
                               join u in _context.Users on g.AppUserId equals u.Id
                               join t in _context.TodoItems on g.GroupId equals t.GroupModelId
                               where u.Id == userId
                               where t.Finished == true
                               select t.Id).Count();

            return activeTasks;
        }

        public ICollection<string> GetUserTaskCount(string userId)
        {
            var currentTasks = (from g in _context.Groups
                                join u in _context.Users on g.AppUserId equals u.Id
                                join t in _context.TodoItems on g.GroupId equals t.GroupModelId
                                select new
                                {
                                    u.Id
                                });
            
            throw new NotImplementedException();

            /*join t in _context.TodoItems on g.GroupId equals t.GroupModelId
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
            }).ToList();*/
            //return currentTasks;
        }
    }
}
