using ConsoleAdventureManager.Domain;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace QuestTracker.Persistence;

public class JsonStorage
{
    private readonly string _path;
    private Dictionary<string, User> _users = new(StringComparer.OrdinalIgnoreCase);

    public JsonStorage(string path) => _path = path;

    public void Load()
    {
        if (!File.Exists(_path)) return;
        var json = File.ReadAllText(_path);
        var list = JsonSerializer.Deserialize<List<User>>(json, new JsonSerializerOptions
        {
            Converters = { new JsonStringEnumConverter() }
        }) ?? new();
        _users = new(StringComparer.OrdinalIgnoreCase);
        foreach (var u in list) _users[u.Username] = u;
    }

    public void Save()
    {
        var list = _users.Values.ToList();
        var json = JsonSerializer.Serialize(list, new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() }
        });
        File.WriteAllText(_path, json);
    }

    public User? Get(string username) => _users.TryGetValue(username, out var u) ? u : null;

    public void Upsert(User u) => _users[u.Username] = u;

    public IEnumerable<User> All() => _users.Values;
}
