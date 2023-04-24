using System.Collections.Generic;
using StoreModuleClient.Models;
using StoreModuleClient.ViewModels.Base;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace StoreModuleClient.ViewModels.UserControlsViewModels;

public class NestedQueriesUserControlViewModel : ViewModel
{
    private List<Product> _where;
    private List<Manufacturer> _from;
    private List<ProductManufacturerView> _select;

    public List<Product> Where
    {
        get => _where;
        set => SetField(ref _where, value);
    }

    public List<Manufacturer> From
    {
        get => _from;
        set => SetField(ref _from, value);
    }

    public List<ProductManufacturerView> Select
    {
        get => _select;
        set => SetField(ref _select, value);
    }

    public NestedQueriesUserControlViewModel()
    {
        using (var context = new ApplicationContext())
        {
            Where = context.Set<Product>()
                .FromSqlRaw(
                    "SELECT * FROM Products WHERE ManufacturerId = (SELECT ID FROM Manufacturers WHERE ProductsCount > 2)")
                .ToList();
            From = context.Set<Manufacturer>()
                .FromSqlRaw("SELECT * FROM (SELECT * FROM Manufacturers) m WHERE m.Title LIKE 'A%'").ToList();
            Select = (context.Set<ProductManufacturerView>().ToList()).ToList();
        }
    }
}