using System;

namespace StoreModuleClient.Models;

public partial class ProductsView
{
    public string Product { get; set; } = null!;

    public string Serial { get; set; } = null!;

    public int Cost { get; set; }

    public DateOnly DateOfUpdate { get; set; }

    public string Manufacturer { get; set; } = null!;

    public string Country { get; set; } = null!;

    public int Count { get; set; }
}
