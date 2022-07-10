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

    public DbSet<Item> Items { get; set; }
}
