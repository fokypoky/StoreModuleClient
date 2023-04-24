using System.Collections.Generic;

namespace StoreModuleClient.Models;

public partial class Manufacturer
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Country { get; set; } = null!;

    public int ProductsCount { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
