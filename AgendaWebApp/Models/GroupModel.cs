using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaWebApp.Models
{
    public class GroupModel
    {
        /// <summary>
        /// Primary key of group
        /// </summary>
        [Key]
        public int GroupId { get; set; }
/*
        /// <summary>
        /// Group Id
        /// </summary>
        public int GroupId { get; set; }*/

        /// <summary>
        /// Name of group
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of group
        /// </summary>
        public string Description { get; set; }

/*        /// <summary>
        /// Todo items associated with the group
        /// </summary>
        [ForeignKey("TodoItemModel")]
        public int? TodoItemId { get; set; }
        public ICollection<TodoItemModel> TodoItem { get; set; }

        /// <summary>
        /// App users associated with the group
        /// </summary>
        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public ICollection<AppUser> AppUser { get; set; }*/
    }
}
