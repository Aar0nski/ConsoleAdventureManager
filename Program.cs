using ConsoleAdventureManager.Domain;
using ConsoleAdventureManager.Services;

namespace ConsoleAdventureManager
{
    internal class Program
    {
        private static NotificationService _notification = new TwilioNotificationService();

        static void Main(string[] args)
        {
            var hero = new User { Username = "Aaron", Email = "Hero@Example.com", Phone = "+4670XXXXXXX" };
            _notification.SendTwoFactorCode(hero, "1234");
            Console.WriteLine("Programmet körs utan fel ✅");
        }
    }
}
