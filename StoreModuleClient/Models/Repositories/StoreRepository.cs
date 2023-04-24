using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StoreModuleClient.Models.Repositories.Base;
using StoreModuleClient.Repositories.Base;

namespace StoreModuleClient.Models.Repositories;

public class StoreRepository : Repository<Store>
{
    public StoreRepository(DbContext context) : base(context)
    {
    }

    public ICollection<Store> GetAll()
    {
        return _context.Set<Store>()
            .Include(s => s.Employee)
            .Include(s => s.Product)
            .ToList();
    }

    public override void Add(Store store)
    {
        try
        {
            _context.Database
                .ExecuteSqlRaw(
                    "EXECUTE dbo.AddProductToStore @product_serial, @amount, @employee_passport",
                    new SqlParameter("@product_serial", store.Product.SerialNumber),
                    new SqlParameter("@amount", store.Amount),
                    new SqlParameter("@employee_passport", store.Employee.Passport)
                );
        }
        catch(Exception e)
        {
            MessageBox.Show(e.Message);
        }
    }

    public override void Remove(Store store)
    {
        try
        {
            _context.Database
                .ExecuteSqlRaw("EXECUTE dbo.RemoveFromStore @product_serial, @employee_pass",
                    new SqlParameter("@product_serial", store.Product.SerialNumber),
                    new SqlParameter("@employee_pass", store.Employee.Passport));
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
    }

    public void Update(Store store, int newAmount)
    {
        try
        {
            _context.Database
                .ExecuteSqlRaw(
                    "EXECUTE dbo.UpdateProductAmountInStore @product_serial, @employee_pass, @amount",
                    new SqlParameter("@product_serial", store.Product.SerialNumber),
                    new SqlParameter("@employee_pass", store.Employee.Passport),
                    new SqlParameter("@amount", newAmount)
                );
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
    }
}