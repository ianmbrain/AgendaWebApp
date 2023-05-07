using Microsoft.AspNetCore.Identity;

namespace AgendaWebApp.Models
{
    public class AppUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public ICollection<GroupModel> Groups { get; set; }
    }
}
