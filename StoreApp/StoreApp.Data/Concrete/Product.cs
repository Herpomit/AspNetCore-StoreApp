﻿using System.ComponentModel.DataAnnotations.Schema;

namespace StoreApp.Data.Concrete;

public class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }

    public List<Category> Categories { get; set; } = new();
}
