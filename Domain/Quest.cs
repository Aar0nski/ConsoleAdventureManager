namespace ConsoleAdventureManager.Domain;


public enum Priority { High, Medium, Low }

public class Quest
{
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public DateTime DueDate { get; set; }
    public Priority Priority { get; set; } = Priority.Medium;
    public bool IsCompleted { get; set; } = false;

    // boolen gör en kontroll om uppdraget är nära deadline genom att jämföra nuvarande tid med förfallodatumet.
    public bool IsDueSoon(int hours) =>
        !IsCompleted && (DueDate - DateTime.Now).TotalHours <= hours;
}