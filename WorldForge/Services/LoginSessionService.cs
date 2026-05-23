namespace WorldForge.Services
{
    public class LoginSessionService
    {
        public void SetLoginSession(ISession session, string userId, string email, bool isAdmin)
        {
            session.SetString("UserId", userId);
            session.SetString("Email", email);
            session.SetString("IsLoggedIn", "true");
            session.SetString("IsAdmin", isAdmin ? "true" : "false");
        }
    }
}
