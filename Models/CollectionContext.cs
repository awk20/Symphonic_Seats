using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SymphonicSeats2.Models;
public class CollectionContext : IdentityDbContext<IdentityUser>
{

    // Constructor including databse connection stream
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
            Location = "San Francisco, California",
            Price = 350
        },
        new CollectionItem
        {
            Id = 2,
            Name = "Weezer",
            Description = "Weezer is back on tour performing their latest album SZNZ.",
            ConcertTime = new DateTime(2023, 10, 15),
            ImageURL = "https://s3.amazonaws.com/heights-photos/wp-content/uploads/2019/03/03130519/Weezer.jpg",
            Location = "Austin, Texas",
            Price = 200
        },
        new CollectionItem
        {
            Id = 3,
            Name = "The Cure",
            Description = "Kings of the alternative genre, The Cure are back on tour for the first time in years.",
            ConcertTime = new DateTime(2023, 09, 21),
            ImageURL = "https://blog.roughtrade.com/content/images/size/w1000/2022/02/Screen-Shot-2022-02-14-at-9.21.39-AM.png",
            Location = "Miami, Florida",
            Price = 175
        }
        /*         new CollectionItem
                {
                    Id = 6,
                    Name = "Blur",
                    Description = "Back for the first time in more than a decade, Brit Pop band Blur is on tour for their first album in quite a while",
                    ConcertTime = new DateTime(2023, 10, 2),
                    ImageURL = "https://upload.wikimedia.org/wikipedia/commons/5/5b/BlurWembley090723_%28166_of_172%29_%28cropped%29.jpg",
                    Location = "Ontario, Canada",
                    Price = 175
                } */
        );

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<CollectionItem> CollectionItems => Set<CollectionItem>();

}