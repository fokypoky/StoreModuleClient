using System;
using System.Collections.Generic;

namespace StoreModuleClient.Models;

public partial class Product
{
    public int Id { get; set; }

    public string ProductTitle { get; set; } = null!;

    public string SerialNumber { get; set; } = null!;

    public int Cost { get; set; }

    public int ManufacturerId { get; set; }

    public DateOnly UpdateDate { get; set; }

    public virtual Manufacturer Manufacturer { get; set; } = null!;

    public virtual ICollection<Store> Stores { get; } = new List<Store>();
}
