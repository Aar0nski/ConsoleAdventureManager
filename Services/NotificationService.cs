using ConsoleAdventureManager.Domain;

namespace ConsoleAdventureManager.Services
{
    public class NotificationService
    {
        // 📨 Simulerad standardnotis (konsol)
        public virtual void SendDeadlineReminder(User user, Quest quest)
        {
            Console.WriteLine($"📧 {user.Email}: ⚔️ Hjälte, '{quest.Title}' måste vara klart imorgon!");
        }

        // 🔐 Simulerad 2FA-kod (konsol)
        public virtual void SendTwoFactorCode(User user, string code)
        {
            Console.WriteLine($"📧 {user.Email}: Din 2FA-kod är {code}");
        }
    }
}
