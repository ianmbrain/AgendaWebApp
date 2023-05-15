﻿using Microsoft.AspNetCore.Identity;
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

        /// <summary>
        /// Name of group
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of group
        /// </summary>
        public string Description { get; set; }

        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public IdentityUser? AppUser { get; set; }
    }
}
