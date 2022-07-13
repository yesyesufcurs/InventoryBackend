// <copyright file="DataContext.cs" company="Vincent (yesyesufcurs)">
// Copyright (c) Vincent (yesyesufcurs). All rights reserved.
// </copyright>

namespace InventoryV3.Data;

using InventoryV3.Models;
using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    /// <summary>
    /// Gets or sets the <see cref="Item"/> table.
    /// </summary>
    public DbSet<Item> Items { get; set; }

}
