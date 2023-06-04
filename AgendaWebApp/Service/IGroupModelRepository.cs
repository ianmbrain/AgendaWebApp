using AgendaWebApp.Data.Enum;
using AgendaWebApp.Models;

namespace AgendaWebApp.Service
{
    public interface IGroupModelRepository
    {
        ICollection<GroupModel> GetAll();
        bool Add(GroupModel item);
        bool Save();
    }
}
