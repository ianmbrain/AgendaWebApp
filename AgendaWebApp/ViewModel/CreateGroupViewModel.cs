using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaWebApp.ViewModel
{
    public class CreateGroupViewModel
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AppUserId { get; set; }
    }
}
