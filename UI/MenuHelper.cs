namespace ConsoleAdventureManager.UI;

public static class MenuHelper
{
    public static void Header(string title)
    {
        Console.WriteLine(new string('=', 42)); // Skapar en rad med '=' tecken för att fungera som en visuell separator i konsolen.
        Console.WriteLine(title); // Skriver ut titeln som skickas in som parameter till konsolen.
        Console.WriteLine(new string('=', 42)); // Skapar ytterligare en rad med '=' tecken för att avsluta sektionen.
    }
    public static void Ok(string m) => Color(m, ConsoleColor.Green); // Metod för att skriva ut ett meddelande i grönt färg för att indikera framgång.
    public static void Warn(string m) => Color(m, ConsoleColor.Yellow); // Metod för att skriva ut ett varningsmeddelande i gult färg.
    public static void Info(string m) => Color(m, ConsoleColor.Cyan); // Metod för att skriva ut ett informationsmeddelande i cyan färg.

    public static string ReadHidden() // Metod för att läsa in användarinmatning utan att visa tecknen i konsolen (användbart för lösenord).
    {
        var s = "";
        while (true)
        {
            var k = Console.ReadKey(true);
            if (k.Key == ConsoleKey.Enter) { Console.WriteLine(); break; } // Avsluta inmatning vid Enter och gå till ny rad
            else
            if (k.Key == ConsoleKey.Backspace && s.Length > 0) { s = s[..^1]; Console.Write("\b \b"); } // Hantera backspace genom att ta bort sista tecknet och uppdatera konsolen
            else if (!char.IsControl(k.KeyChar)) { s += k.KeyChar; Console.Write("*"); } // Lägg till tecken och visa '*' genom att ersätta det med en asterisk
        }
        return s;
    }
    private static void Color(string m, ConsoleColor c) // Hjälpmetod för att skriva ut meddelanden i en specifik färg
    {
        var prev = Console.ForegroundColor;
        Console.ForegroundColor = c; Console.WriteLine(m); Console.ForegroundColor = prev; // Sätt färgen, skriv ut meddelandet och återställ den ursprungliga färgen
    }
}