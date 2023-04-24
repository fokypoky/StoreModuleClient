using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StoreModuleClient.Models.Repositories.Base;
using StoreModuleClient.Repositories.Base;

namespace StoreModuleClient.Models.Repositories;

public class ManufacturerRepository : Repository<Manufacturer>
{
    public ManufacturerRepository(DbContext context) : base(context)
    {
    }

    public ICollection<Manufacturer> GetAll() => _context.Set<Manufacturer>().ToList();
    public override void Add(Manufacturer manufacturer)
    {
        try
        {
            _context.Database.ExecuteSqlRaw
                ($"EXECUTE dbo.AddManufacturer @title, @country",
                    new SqlParameter("@title", manufacturer.Title),
                    new SqlParameter("@country", manufacturer.Country));
            MessageBox.Show($"Manufacturer {manufacturer.Title} added");
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
    }

    public override void Remove(Manufacturer manufacturer)
    {
        try
        {
            _context.Database.ExecuteSql($"EXECUTE dbo.RemoveManufacturer @title={manufacturer.Title}");
            MessageBox.Show($"Manufacturer {manufacturer.Title} removed");
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
    }

    public void Update(string oldTitle, string newTitle)
    {
        try
        {
            _context.Database.ExecuteSqlRaw($"EXECUTE dbo.UpdateManufacturerTitle @oldTitle, @newTitle",
                new SqlParameter("@oldTitle", oldTitle),
                new SqlParameter("@newTitle", newTitle));
            MessageBox.Show("OK");
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
    }
}