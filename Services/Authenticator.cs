using ConsoleAdventureManager.Domain;
using ConsoleAdventureManager.Services;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleAdventureManager.Services;

public class Authenticator
{
    private readonly Persistence.JsonStorage _storage; // Användardatalagring
    private readonly NotificationService _notifier; // Notifikationstjänst
    private static readonly TimeSpan TwoFactorValidity = TimeSpan.FromMinutes(5); // 2FA-kod giltighetstid

    public Authenticator(Persistence.JsonStorage storage, NotificationService notifier) 
    {
        _storage = storage; // Initiera lagring
        _notifier = notifier; // Initiera notifieringstjänst
    }

    public (bool ok, string msg) Register(string username, string email, string? phone, string password)
    {
        if (_storage.Get(username) is not null) return (false, "Användarnamnet är upptaget."); // Kontrollera om användarnamnet redan finns
        var (strong, why) = ValidatePasswordStrength(password); // Validera lösenordets styrka
        if (!strong) return (false, $"Svagt lösenord: {why}"); // Återvänd felmeddelande om lösenordet är svagt

        var salt = Convert.ToBase64String(RandomNumberGenerator.GetBytes(16)); // Generera en slumpmässig salt, salt är en slumpmässig data som läggs till lösenordet innan hashning för att öka säkerheten
        var hash = Hash(password, salt); // Hasha lösenordet med saltet, hasha är processen att omvandla data till en fast längd sträng av tecken

        var u = new User
        {
            Username = username,
            Email = email,
            Phone = string.IsNullOrWhiteSpace(phone) ? null : phone,
            PasswordSalt = salt,
            PasswordHash = hash
        };
        _storage.Upsert(u);
        _storage.Save();
        return (true, "Registrerad!"); // Återvänd framgångsmeddelande
    }

    public (bool ok, User? user, string msg) LoginPassword(string username, string password) // Inloggning med användarnamn och lösenord
    {
        var u = _storage.Get(username);
        if (u is null) return (false, null, "Okänd användare.");
        if (Hash(password, u.PasswordSalt) != u.PasswordHash) return (false, null, "Fel lösenord.");
        return (true, u, "Lösenord OK");
    }

    public void StartTwoFactor(User user)
    {
        var code = RandomNumberGenerator.GetInt32(1000, 9999).ToString("0000"); // Generera en 4-siffrig slumpmässig kod
        user.LastTwoFactorCode = code; // Sätt den senaste 2FA-koden
        user.TwoFactorExpiry = DateTime.Now.Add(TwoFactorValidity); // Sätt kodens utgångstid
        _storage.Upsert(user); // Uppdatera användardata
        _storage.Save(); // Spara ändringar
        _notifier.SendTwoFactorCode(user, code); // Skicka koden via notifieringstjänsten
    }

    public bool VerifyTwoFactor(User user, string input) // Verifiera den angivna 2FA-koden
    {
        return user.LastTwoFactorCode == input && // Kontrollera om den angivna koden matchar den senaste koden genom att jämföra dem
               user.TwoFactorExpiry is not null && // Kontrollera om utgångstiden är satt gemom att den inte är null 
               user.TwoFactorExpiry > DateTime.Now; // Kontrollera om koden fortfarande är giltig genom att jämföra utgångstiden med den aktuella tiden
    }

    public static (bool ok, string why) ValidatePasswordStrength(string password)
    {
        if (password.Length < 6) return (false, "minst 6 tecken");
        if (!password.Any(char.IsDigit)) return (false, "minst 1 siffra");
        if (!password.Any(char.IsUpper)) return (false, "minst 1 stor bokstav");
        if (!password.Any(ch => !char.IsLetterOrDigit(ch))) return (false, "minst 1 specialtecken");
        return (true, "OK");
    }

    private static string Hash(string pwd, string salt)
    {
        using var sha = SHA256.Create();// Skapa en SHA256-hashinstans för att hasha lösenordet
        return Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(pwd + salt))); // Kombinera lösenordet med saltet, konvertera till byte-array och hasha det, sedan konvertera den hashede byte-arrayen till en Base64-sträng för lagring
    }
}
