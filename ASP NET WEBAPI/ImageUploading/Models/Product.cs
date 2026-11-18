using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ImageUploading.Models;

public partial class Product
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public decimal Price { get; set; }
    [Required]
    public string? ImagePath { get; set; }
}
