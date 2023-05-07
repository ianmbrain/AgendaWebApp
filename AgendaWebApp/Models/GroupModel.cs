using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaWebApp.Models
{
    public class GroupModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [ForeignKey("TodoItemModel")]
        public TodoItemModel TodoItem { get; set; }

        [ForeignKey("AppUser")]
        public AppUser AppUser { get; set; }
    }
}
