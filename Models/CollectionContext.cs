using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;

namespace SymphonicSeats2.Models;
public class CollectionContext : DbContext
{

    // Constructor including dtaabse connection stream
    public CollectionContext(DbContextOptions<CollectionContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CollectionItem>().HasData(
        new CollectionItem
        {
            Id = 1,
            Name = "Metallica",
            Description = "See metal legends Metallica in concert in San Francisco.",
            ImageURL = "https://cdn-aeoeg.nitrocdn.com/CEyMkTCvQqLzCiakJhTISKVqIxcNYCml/assets/images/optimized/rev-9a82380/secureyourtrademark.com/wp-content/uploads/2022/07/img-metallica-trademarks.jpg",
            ConcertTime = new DateTime(2023, 12, 18),
            Location = "San Francisco, California"
        },
        new CollectionItem
        {
            Id = 2,
            Name = "Weezer",
            Description = "Weezer is back on tour performing their latest album SZNZ.",
            ConcertTime = new DateTime(2023, 10, 15),
            ImageURL = "https://s3.amazonaws.com/heights-photos/wp-content/uploads/2019/03/03130519/Weezer.jpg",
            Location = "Austin, Texas"
        },
        new CollectionItem
        {
            Id = 3,
            Name = "The Cure",
            Description = "Kings of the alternative genre, The Cure are back on tour for the first time in years.",
            ConcertTime = new DateTime(2023, 09, 21),
            ImageURL = "https://blog.roughtrade.com/content/images/size/w1000/2022/02/Screen-Shot-2022-02-14-at-9.21.39-AM.png",
            Location = "Miami, Florida"
        }
        );

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<CollectionItem> CollectionItems => Set<CollectionItem>();

}