// <copyright file="Invoice.cs" company="Vincent (yesyesufcurs)">
// Copyright (c) Vincent (yesyesufcurs). All rights reserved.
// </copyright>

namespace InventoryV3.Models;

/// <summary>
/// Models how the invoice is saved in the datbase.
/// </summary>
public class Invoice
{
    public int Id { get; set; }

    public int ItemId { get; set; }

    public virtual Item Item { get; set; }

    public string FileExtension { get; set; }

    public byte[] Data { get; set; }
}
