using ConsoleAdventureManager.Domain;

namespace ConsoleAdventureManager.Domain;

public class User
{
    public string Username { get; set; } = "";
    public string Email { get; set; } = "";
    public string? Phone { get; set; }

    // Auth variabler för användaren
    public string PasswordHash { get; set; } = "";
    public string PasswordSalt { get; set; } = "";

    // 2fa variabler för användaren
    public string? LastTwoFactorCode { get; set; }
    public DateTime? TwoFactorExpiry { get; set; }

    // Den listan av uppdrag för användaren
    public List<Quest> Quests { get; set; } = new();
}
