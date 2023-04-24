using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StoreModuleClient.Models.Repositories.Base;

namespace StoreModuleClient.Models.Repositories;

public class ProductsViewRepository : Repository<ProductsView>
{
    public ProductsViewRepository(DbContext context) : base(context)
    {
    }

    public ICollection<ProductsView> GetAll() => _context.Set<ProductsView>().ToList();
    
}