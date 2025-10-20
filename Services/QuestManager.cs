using ConsoleAdventureManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAdventureManager.Services
{
    public class QuestManager
    {
        private readonly List<Quest> _list = new(); // Intern lista för uppdrag readonly gör att referensen inte kan ändras och pekar alltid på samma lista.

        public void Load(IEnumerable<Quest> quests) // Laddar uppdrag från en källa (inte implementerad här)
        {
            _list.Clear(); // Rensa befintliga uppdrag
            _list.AddRange(quests); // Lägg till nya uppdrag
        }

        public IEnumerable<Quest> All() => _list; // Returnerar alla uppdrag

        public void Add(Quest q) => _list.Add(q); // Lägger till ett nytt uppdrag

        public bool Complete(string title) // Markerar ett uppdrag som slutfört baserat på titeln
        {
            var q = _list.FirstOrDefault(x => x.Title.Equals(title, StringComparison.OrdinalIgnoreCase)); // Hitta uppdraget (case-insensitive) annars returnera null.
            if (q is null) return false;
            q.IsCompleted = true; return true;
        }
    }
}