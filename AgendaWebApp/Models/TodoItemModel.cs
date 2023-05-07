using AgendaWebApp.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace AgendaWebApp.Models
{
    public class TodoItemModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// Field set to one of the values from ImportanceEnum.
        /// </summary>
        public ImportanceEnum Importance { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? FinishedDate { get; set; }

        public bool? Finished { get; set; }
    }
}
