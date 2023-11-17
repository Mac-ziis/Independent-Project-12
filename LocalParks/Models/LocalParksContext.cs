using Microsoft.EntityFrameworkCore;

namespace  LocalParks.Models
{
  public class LocalParksContext : DbContext
  {
    public DbSet<Park> Parks {get; set;}

    public LocalParksContext(DbContextOptions<LocalParksContext> options) : base(options)
    {
    }

      protected override void OnModelCreating(ModelBuilder builder)
      {
        builder.Entity<Park>()
        .HasData(
          new Park { ParkId = 1, Name = "Laurelhurst Park", Location = "SE Portland, at the corner of Stark and Cesar E chavez Blvd", Summary = "Designed by the same man who designed Central prak in NYC, This is a beatiful addition to Portlands central Eastern side."},
          new Park { ParkId = 2, Name = "Grant Park", Location = "NE Portland, At the corner of 33rd and Tillamook", Summary = "Nestled in next to Grant High School, this park offers a nice retreat from the city's hustle and bustle." },
          new Park { ParkId = 3, Name = "Colonel Sumners Park", Location = "SE Portland, at the corner of 20th and Belmont Ave", Summary = "Used to have Thirsty Thursdays here until it got kinda dirty and the cops started showing up..."}
        );
      }
    }
  }