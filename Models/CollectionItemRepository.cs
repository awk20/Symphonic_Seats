using System.Collections.Concurrent;
using System.Text.Json;
using Microsoft.AspNetCore.Http.Features;
using SQLitePCL;

namespace SymphonicSeats2.Models;

public class CollectionItemRepository
{

    private readonly CollectionContext _context;

    //private readonly ConcurrentDictionary<int, int> votes = new ConcurrentDictionary<int, int>();
    public CollectionItemRepository(CollectionContext ctx)
    {
        _context = ctx;
    }

    public IEnumerable<CollectionItem> Get()
    {
        // loads data from disk
        /*         
            var jsonFile = System.IO.File.OpenRead("Data/collection.json");

            return JsonSerializer.Deserialize<CollectionItem[]>(jsonFile); 
        */

        // loads data from database
        return _context.CollectionItems.ToArray();
    }


    // find database elements by id
    public CollectionItem FindById(int id)
    {
        return _context.CollectionItems.FirstOrDefault(i => i.Id == id);
    }

    public int Vote(int id, bool direction)
    {
        // Find elemnt being voted on by id
        var item = FindById(id);

        //System.Console.WriteLine($"Voting for {item.Name}");
        // If item is not found function is over
        if (item == CollectionItem.NotFound)
        {
            return 0;
        }

        //System.Console.WriteLine($"Voting for {item.Name}");

        // if direction is upvote increment Votes by one
        if (direction)
        {
            item.Votes++;
        }
        // Otherwise decrement Votes if direction is downvote
        else
        {
            item.Votes--;
        }

        //System.Console.WriteLine($"The {item.Name} vote total is now {item.Votes}");

        // update the collectionitem and save the changes to it
        _context.CollectionItems.Update(item);
        _context.SaveChanges();

        return item.Votes;
    }
}
