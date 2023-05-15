using AgendaWebApp.Data.Enum;
using AgendaWebApp.Models;

namespace AgendaWebApp.Service
{
    public interface IGroupModelRepository
    {
        ICollection<GroupModel> GetAll();
        GroupModel GetById(int id);
        GroupModel GetByIdNoTracking(int id);
        bool Add(GroupModel item);
        bool Delete(GroupModel item);
        bool Update(GroupModel item);
        bool Save();
    }
}
