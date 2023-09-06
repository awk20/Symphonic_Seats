using System.Text.Json;
using SQLitePCL;

namespace SymphonicSeats2.Models;

public class CollectionItemRepository
{

    private readonly CollectionContext _Context;
    public CollectionItemRepository(CollectionContext ctx)
    {
        _Context = ctx;
    }

    public IEnumerable<CollectionItem> Get()
    {
        // loads data from disk
        /*         
            var jsonFile = System.IO.File.OpenRead("Data/collection.json");

            return JsonSerializer.Deserialize<CollectionItem[]>(jsonFile); 
        */

        // loads data from database
        return _Context.CollectionItems;
    }

}
