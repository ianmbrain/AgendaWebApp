using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AgendaWebApp.Models
{
    public class AppUser
    {
        [Key]
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public ICollection<GroupModel> Groups { get; set; }
    }
}
