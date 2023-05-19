using AgendaWebApp.Models;
using Microsoft.AspNetCore.Identity;

namespace AgendaWebApp.Service
{
    public interface IUserRepository
    {
        ICollection<IdentityUser> GetAll();
        ICollection<string> GetUserTaskCount(string Id);
    }
}
