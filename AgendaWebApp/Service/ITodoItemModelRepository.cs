using AgendaWebApp.Data.Enum;
using AgendaWebApp.Models;

namespace AgendaWebApp.Service
{
    public interface ITodoItemModelRepository
    {
        Task<IEnumerable<TodoItemModel>> GetAll();
        Task<TodoItemModel> GetByIdAsync(int id);
        Task<TodoItemModel> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<TodoItemModel>> GetItemByImportance(ImportanceEnum importance);
        Task<IEnumerable<TodoItemModel>> GetItemByGroupId(int id);
        bool Add(TodoItemModel item);
        bool Delete(TodoItemModel item);
        bool Update(TodoItemModel item);
        bool Save();


    }
}
