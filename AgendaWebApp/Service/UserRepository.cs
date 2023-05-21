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

        public int GetActiveMinor(string userId)
        {
            var activeTasks = (from g in _context.Groups
                               join u in _context.Users on g.AppUserId equals u.Id
                               join t in _context.TodoItems on g.GroupId equals t.GroupModelId
                               where u.Id == userId
                               where t.Finished == false
                               where t.Importance == Data.Enum.ImportanceEnum.Minor
                               select t.Id).Count();

            return activeTasks;
        }

        public int GetActiveRelevant(string userId)
        {
            var activeTasks = (from g in _context.Groups
                               join u in _context.Users on g.AppUserId equals u.Id
                               join t in _context.TodoItems on g.GroupId equals t.GroupModelId
                               where u.Id == userId
                               where t.Finished == false
                               where t.Importance == Data.Enum.ImportanceEnum.Relevant
                               select t.Id).Count();

            return activeTasks;
        }

        public int GetActiveImportant(string userId)
        {
            var activeTasks = (from g in _context.Groups
                               join u in _context.Users on g.AppUserId equals u.Id
                               join t in _context.TodoItems on g.GroupId equals t.GroupModelId
                               where u.Id == userId
                               where t.Finished == false
                               where t.Importance == Data.Enum.ImportanceEnum.Important
                               select t.Id).Count();

            return activeTasks;
        }

        public int GetFinishedMinor(string userId)
        {
            var activeTasks = (from g in _context.Groups
                               join u in _context.Users on g.AppUserId equals u.Id
                               join t in _context.TodoItems on g.GroupId equals t.GroupModelId
                               where u.Id == userId
                               where t.Finished == true
                               where t.Importance == Data.Enum.ImportanceEnum.Minor
                               select t.Id).Count();

            return activeTasks;
        }

        public int GetFinishedRelevant(string userId)
        {
            var activeTasks = (from g in _context.Groups
                               join u in _context.Users on g.AppUserId equals u.Id
                               join t in _context.TodoItems on g.GroupId equals t.GroupModelId
                               where u.Id == userId
                               where t.Finished == true
                               where t.Importance == Data.Enum.ImportanceEnum.Relevant
                               select t.Id).Count();

            return activeTasks;
        }

        public int GetFinishedImportant(string userId)
        {
            var activeTasks = (from g in _context.Groups
                               join u in _context.Users on g.AppUserId equals u.Id
                               join t in _context.TodoItems on g.GroupId equals t.GroupModelId
                               where u.Id == userId
                               where t.Finished == true
                               where t.Importance == Data.Enum.ImportanceEnum.Important
                               select t.Id).Count();

            return activeTasks;
        }
    }
}
