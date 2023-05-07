﻿using AgendaWebApp.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? FinishedDate { get; set; }

        public bool? Finished { get; set; }

        [ForeignKey("GroupModel")]
        public int? GroupModelId { get; set; }

    }
}
