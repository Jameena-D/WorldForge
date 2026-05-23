namespace WorldForge.Services
{
    public class AdminAccessService
    {
        public bool IsAdmin(string? isLoggedIn, string? isAdmin)
        {
            return isLoggedIn == "true" && isAdmin == "true";
        }
    }
}
