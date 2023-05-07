using System.ComponentModel.DataAnnotations;

namespace AgendaWebApp.Models
{
    public class TodoItemModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Importance { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime FinishedDate { get; set; }

        public bool Finished { get; set; }
    }
}
