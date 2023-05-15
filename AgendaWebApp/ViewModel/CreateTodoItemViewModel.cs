using AgendaWebApp.Data.Enum;
using AgendaWebApp.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AgendaWebApp.ViewModel
{
    public class CreateTodoItemViewModel
    {
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Field set to one of the values from ImportanceEnum.
        /// </summary>
        [Required, EnumDataType(typeof(ImportanceEnum))]
        public ImportanceEnum Importance { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        [CheckDateRange()]
        [Required]
        //[Compare(nameof(CreationDate), ErrorMessage = "End date must be equal to or greater than start date")]
        public DateTime? FinishedDate { get; set; }

        public bool Finished { get; set; } = false;

        [ForeignKey("GroupModel")]
        public int? GroupModelId { get; set; }
    }

    public class CheckDateRangeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // If the FinishedDate param is null then it does not have a finished date and thus does not need to be validated.
            if (value == null)
            {
                return ValidationResult.Success;
            }

            DateTime dt = (DateTime)value;
            if (dt.Date >= DateTime.UtcNow.Date)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage ?? "Make sure the date is today or later");
        }

    }
}
