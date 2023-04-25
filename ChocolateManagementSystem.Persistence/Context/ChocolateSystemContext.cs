﻿using ChocolateManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChocolateManagementSystem.Persistence.Context
{
    public class ChocolateSystemContext : DbContext
    {

        public ChocolateSystemContext(DbContextOptions<ChocolateSystemContext> options) : base(options)  { }

        public DbSet<ChocolateBar> ChocolateBars { get; set; }
        public DbSet<ChocolateFactory> ChocolateFactories { get; set; }
        public DbSet<Wholesaler> Wholesalers { get; set; }

    }
}