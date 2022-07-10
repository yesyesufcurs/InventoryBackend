// <copyright file="Item.cs" company="Vincent (yesyesufcurs)">
// Copyright (c) Vincent (yesyesufcurs). All rights reserved.
// </copyright>
namespace InventoryV3.Models;

using InventoryV3.Models.Enums;

/// <summary>
/// Models the items in the inventory.
/// </summary>
public class Item
{
    /// <summary>
    /// Gets or sets the Id of the item.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the item.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date of purchase of the item.
    /// </summary>
    public DateTime DateOfPurchase { get; set; }

    /// <summary>
    /// Gets or sets the barcode of the item.
    /// </summary>
    public string? Barcode { get; set; }

    /// <summary>
    /// Gets or sets the category of an item.
    /// </summary>
    public Category Category { get; set; }
}
