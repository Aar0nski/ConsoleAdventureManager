using ConsoleAdventureManager.Domain;

namespace ConsoleAdventureManager.Services
{
    public class NotificationService
    {
        public virtual void SendTwoFactorCode(User user, string code) // Skickar 2FA-kod till användaren genom att skriva ut till konsolen
        {
            Console.WriteLine($"📧 {user.Email}: Din 2FA-kod är {code}"); // Skickar e-postmeddelande med 2FA-koden
            if (!string.IsNullOrWhiteSpace(user.Phone))
                Console.WriteLine($"📱 SMS till {user.Phone}: 2FA-kod {code}");
        }

        public virtual void SendDeadlineReminder(User user, Quest quest) // Skickar påminnelse om uppdragsdeadline
        {
            Console.WriteLine($"📧 {user.Email}: ⚔️ '{quest.Title}' måste vara klart imorgon!"); // Skickar e-postpåminnelse
            if (!string.IsNullOrWhiteSpace(user.Phone))
                Console.WriteLine($"📱 SMS till {user.Phone}: '{quest.Title}' har deadline {quest.DueDate:yyyy-MM-dd}"); // Skickar SMS-påminnelse
        }
    }
}

