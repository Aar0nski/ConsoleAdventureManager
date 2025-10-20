using ConsoleAdventureManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAdventureManager.Services
{
public class GuildAdvisorAI
{
    public string Generate(string title, DateTime due, Priority pr)
    {
        var rush = due <= DateTime.Now.AddDays(1) ? "Tiden är knapp" : "Planera klokt"; // Kontrollera om uppdraget är brådskande genom att jämföra förfallodatumet med nuvarande tid plus en dag.
            return $"Uppdrag: {title}. {rush}. Prioritet {pr}. Slutför före {due:yyyy-MM-dd}."; 
        }

    public Priority SuggestPriority(string title, string desc, DateTime due)
    {
        var h = (due - DateTime.Now).TotalHours; // Beräkna antalet timmar kvar till förfallodatumet genom att subtrahera nuvarande tid från förfallodatumet och konvertera resultatet till totalt antal timmar.
        if (h <= 24) return Priority.High;
        if (h <= 72) return Priority.Medium;
        return Priority.Low;
    }

    public string Summarize(IEnumerable<Quest> qs) // Sammanfattar uppdragsstatus i en sträng 
        {

            // Räknar antalet aktiva uppdrag, uppdrag som förfaller idag och uppdrag som är nära deadline genom att filtrera listan av uppdrag baserat på deras status och förfallodatum.
            var active = qs.Count(q => !q.IsCompleted);
        var dueToday = qs.Count(q => !q.IsCompleted && q.DueDate.Date == DateTime.Now.Date);
        var soon = qs.Count(q => !q.IsCompleted && q.IsDueSoon(24));
        return $"🎒 {active} pågående, {dueToday} idag, {soon} nära deadline.";
    }
}
}