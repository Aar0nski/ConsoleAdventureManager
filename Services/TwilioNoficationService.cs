using System;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using ConsoleAdventureManager.Domain;

namespace ConsoleAdventureManager.Services
{
    public class TwilioNotificationService : NotificationService
    {
        public override void SendTwoFactorCode(User user, string code)
        {
            var accountSid = Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID");
            var authToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN");

            if (string.IsNullOrWhiteSpace(accountSid) || string.IsNullOrWhiteSpace(authToken))
            {
                Console.Error.WriteLine("⚠️ Twilio credentials saknas. Sätt TWILIO_ACCOUNT_SID och TWILIO_AUTH_TOKEN.");
                return;
            }

            TwilioClient.Init(accountSid, authToken);

            if (string.IsNullOrWhiteSpace(user.Phone))
            {
                Console.WriteLine($"⚠️ Ingen telefon angiven för {user.Username}");
                return;
            }

            var from = new PhoneNumber("+16073262957"); // Ditt Twilio-nummer
            var to = new PhoneNumber(user.Phone);
            var body = $"Hej {user.Username}! Din guild-kod är: {code}";

            try
            {
                MessageResource.Create(to: to, from: from, body: body);
                Console.WriteLine($"📱 SMS skickat till {user.Phone}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"❌ Kunde inte skicka SMS: {ex.Message}");
            }
        }

        public override void SendDeadlineReminder(User user, Quest quest)
        {
            if (string.IsNullOrWhiteSpace(user.Phone)) return;

            TwilioClient.Init(
                Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID"),
                Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN")
            );

            var from = new PhoneNumber("+16073262957");
            var to = new PhoneNumber(user.Phone!);
            var body = $"⚔️ Hjälte {user.Username}! Uppdrag '{quest.Title}' har deadline {quest.DueDate:yyyy-MM-dd}!";

            MessageResource.Create(to: to, from: from, body: body);
        }
    }
}
