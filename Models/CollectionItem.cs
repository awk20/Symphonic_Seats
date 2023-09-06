using System.ComponentModel.DataAnnotations;

namespace SymphonicSeats2.Models;

public class CollectionItem
{

    [Required]
    public string Name { get; set; }

    public string? Description { get; set; }

    public DateTime ConcertTime { get; set; }

    public string? ImageURL { get; set; }

    public string? Location { get; set; }

}