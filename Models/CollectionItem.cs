using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SymphonicSeats2.Models;

public class CollectionItem
{
    public static CollectionItem NotFound = new CollectionItem();

    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public string? Description { get; set; }

    public DateTime ConcertTime { get; set; }

    public string? ImageURL { get; set; }

    public string? Location { get; set; }

    // Not mapped makes sure it doesnt get written to he databse and doesnt try to load it from db
    public int Votes { get; set; }

}