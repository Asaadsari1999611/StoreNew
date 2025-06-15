using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreNew.Models;

public partial class Category
{
    public decimal Id { get; set; }

    public string? Gategoryname { get; set; }

    public string? Imagepath { get; set; }
    [NotMapped]
    public IFormFile Imagefile { get; set; }
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
