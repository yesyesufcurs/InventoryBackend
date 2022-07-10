// <copyright file="DataContext.cs" company="Vincent (yesyesufcurs)">
// Copyright (c) Vincent (yesyesufcurs). All rights reserved.
// </copyright>

using InventoryV3.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryV3.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Invoice>()
            .Navigation(t => t.Item).AutoInclude();
    }

    /// <summary>
    /// Gets or sets the <see cref="Item"/> table.
    /// </summary>
    public DbSet<Item> Items { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="Invoice"/> table.
    /// </summary>
    public DbSet<Invoice> Invoices { get; set; }
}
