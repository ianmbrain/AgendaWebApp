using AgendaWebApp.Models;

namespace AgendaWebApp.Service
{
    public interface IGroupUserRepository
    {
        Task<IEnumerable<GroupModel>> GetGroupsByUser(string userId);
    }
}
