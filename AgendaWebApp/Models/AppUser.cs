using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaWebApp.Models
{
    public class AppUser 
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        [ForeignKey("GroupModelId")]
        public int GroupId { get; set; }
        public ICollection<GroupModel> Groups { get; set; }
    }
}
