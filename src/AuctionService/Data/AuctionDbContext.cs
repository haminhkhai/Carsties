﻿using AuctionService.Entities;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Data;

public class AuctionDbContext : DbContext
{
    public AuctionDbContext(DbContextOptions options)
        : base(options) { }

    public DbSet<Auction> Auctions { get; set; }

    //Items DbSet is optional because Items are related to the
    //Auctions and entity framework will automatically create Items table
    // public DbSet<Item> Items { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //message outbox persistence
        //it'll add 3 tables to database and responsible for outbox functionality
        //remember to add migration for this
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();
    }
}
