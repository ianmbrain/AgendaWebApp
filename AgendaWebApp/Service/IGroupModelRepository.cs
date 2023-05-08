using AgendaWebApp.Data.Enum;
using AgendaWebApp.Models;

namespace AgendaWebApp.Service
{
    public interface IGroupModelRepository
    {
        Task<IEnumerable<GroupModel>> GetAll();
        Task<GroupModel> GetByIdAsync(int id);
        Task<GroupModel> GetByIdAsyncNoTracking(int id);
        bool Add(GroupModel item);
        bool Delete(GroupModel item);
        bool Update(GroupModel item);
        bool Save();
    }
}
