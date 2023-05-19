using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaWebApp.Models
{
    public class GroupModel
    {
        [Key]
        public int GroupId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public IdentityUser? AppUser { get; set; }
    }
}
