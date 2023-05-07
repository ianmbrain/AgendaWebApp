using AgendaWebApp.Data.Enum;
using AgendaWebApp.Models;

namespace AgendaWebApp.Service
{
    public interface ITodoItemModelRepository
    {
        Task<IEnumerable<TodoItemModel>> GetAll();
        Task<TodoItemModel> GetByIdAsync(int id);
        Task<IEnumerable<TodoItemModel>> GetItemByImportance(ImportanceEnum importance);
        bool Add(TodoItemModel item);
        bool Delete(TodoItemModel item);
        bool Save();


    }
}
