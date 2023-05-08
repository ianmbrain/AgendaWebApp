using AgendaWebApp.Data.Enum;
using AgendaWebApp.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AgendaWebApp.ViewModel
{
    public class EditItemViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [Required, EnumDataType(typeof(ImportanceEnum))]
        public ImportanceEnum Importance { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        [Required, DataType(DataType.Date)]
        public DateTime? FinishedDate { get; set; }

        public bool? Finished { get; set; }

        public int? GroupModelId { get; set; }
    }
}
