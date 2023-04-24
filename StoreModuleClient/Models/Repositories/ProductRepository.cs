using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StoreModuleClient.Models.Repositories.Base;
using StoreModuleClient.Repositories.Base;

namespace StoreModuleClient.Models.Repositories;

public class ProductRepository : Repository<Product>
{
    public ProductRepository(DbContext context) : base(context)
    {
    }

    public ICollection<Product> GetAll() => _context.Set<Product>().Include(p => p.Manufacturer).ToList();
    public override void Add(Product product)
    {
        try
        {
            _context.Database.ExecuteSqlRaw(
                "EXECUTE dbo.AddProduct @productTitle, @serialNumber, @cost, @manufacturerTitle",
                new SqlParameter("@productTitle", product.ProductTitle),
                new SqlParameter("@serialNumber", product.SerialNumber),
                new SqlParameter("@cost", product.Cost),
                new SqlParameter("@manufacturerTitle", product.Manufacturer.Title));
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
    }

    public override void Remove(Product product)
    {
        try
        {
            _context.Database
                .ExecuteSqlRaw(
                    "EXECUTE dbo.RemoveProduct @serial",
                    new SqlParameter("@serial", product.SerialNumber)
                );
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
    }

    public void Update(Product product, int newCost)
    {
        try
        {
            _context.Database
                .ExecuteSqlRaw(
                    "EXECUTE dbo.UpdateProductCost @serial, @newCost",
                    new SqlParameter("@serial", product.SerialNumber),
                    new SqlParameter("@newCost", newCost)
                );
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
    }
}