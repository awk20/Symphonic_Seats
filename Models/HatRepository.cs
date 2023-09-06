using System.Text.Json;

namespace SymphonicSeats2.Models;

public class HatRepository
{
    public IEnumerable<CollectionItem> Get()
    {
        var jsonFile = System.IO.File.OpenRead("Data/collection.json");

        return JsonSerializer.Deserialize<CollectionItem[]>(jsonFile);
    }
}