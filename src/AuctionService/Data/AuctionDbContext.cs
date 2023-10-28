using AuctionService.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Data;

public class AuctionDbContext : DbContext
{
  public AuctionDbContext(DbContextOptions options) : base(options)
  {
  }

  public DbSet<Auction> Auctions { get; set; }
  //Items DbSet is optional because Items are related to the 
  //Auctions and entity framework will automatically create Items table
  // public DbSet<Item> Items { get; set; }
}
