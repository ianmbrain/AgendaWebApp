using AgendaWebApp.Models;
using Microsoft.AspNetCore.Identity;

namespace AgendaWebApp.Service
{
    public interface IUserRepository
    {
        ICollection<IdentityUser> GetAll();
        int GetActiveTaskCount(string userId);
        int GetActiveMinor(string userId);
        int GetActiveRelevant(string userId);
        int GetActiveImportant(string userId);
        int GetFinishedTaskCount(string userId);
    }
}
