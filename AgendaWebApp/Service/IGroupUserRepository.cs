using AgendaWebApp.Models;

namespace AgendaWebApp.Service
{
    public interface IGroupUserRepository
    {
        ICollection<GroupModel> GetGroupsByUser(string userId);
    }
}
