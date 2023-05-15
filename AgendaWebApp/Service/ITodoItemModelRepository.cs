using AgendaWebApp.Data.Enum;
using AgendaWebApp.Models;

namespace AgendaWebApp.Service
{
    public interface ITodoItemModelRepository
    {
        ICollection<TodoItemModel> GetAll();
        ICollection<TodoItemModel> GetAllByUser();
        TodoItemModel GetById(int id);
        TodoItemModel GetByIdNoTracking(int id);
        ICollection<TodoItemModel> GetItemByImportance(ImportanceEnum importance);
        ICollection<TodoItemModel> GetItemByGroupId(int id);
        bool Add(TodoItemModel item);
        bool Delete(TodoItemModel item);
        bool Update(TodoItemModel item);
        bool Save();


    }
}
