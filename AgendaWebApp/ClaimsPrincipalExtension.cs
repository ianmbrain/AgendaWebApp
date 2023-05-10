using System.Security.Claims;

namespace AgendaWebApp
{
    public static class ClaimsPrincipalExtension
    {
        /// <summary>
        /// Returns the user id of the user it is called on
        /// </summary>
        /// <param name="user"> User to return the id of </param>
        /// <returns> User id of the user the method is called on </returns>
        public static string GetUserId(this ClaimsPrincipal user) => user.FindFirst(ClaimTypes.NameIdentifier).Value;
    }
}
