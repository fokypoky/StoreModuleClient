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

public class EmployeeRepository : Repository<Employee>
{
    public EmployeeRepository(DbContext context) : base(context)
    {
    }

    public ICollection<Employee> GetAll() => _context.Set<Employee>().ToList();
    public void Add(Employee employee, DateTime birthday)
    {
        string date = $"{birthday.Year}-{birthday.Month}-{birthday.Day}";
        try
        {
            _context.Database
                .ExecuteSqlRaw(
                    "EXECUTE dbo.AddEmployee @name, @pass, @birthday",
                    new SqlParameter("@name", employee.EmployeeName),
                    new SqlParameter("@pass", employee.Passport),
                    new SqlParameter("@birthday", birthday)
                );
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
    }

    public override void Remove(Employee employee)
    {
        try
        {
            _context.Database
                .ExecuteSqlRaw(
                    "EXECUTE dbo.RemoveEmployee @pass",
                    new SqlParameter("@pass", employee.Passport)
                );
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
    }

    public void Update(Employee oldEmployee, Employee newEmployee)
    {
        try
        {
            _context.Database
                .ExecuteSqlRaw(
                    "EXECUTE dbo.UpdateEmployee @oldPass, @newName, @newPass, @newBirthday",
                    new SqlParameter("@oldPass", oldEmployee.Passport),
                    new SqlParameter("@newName", newEmployee.EmployeeName),
                    new SqlParameter("@newPass", newEmployee.Passport),
                    new SqlParameter("@newBirthday", newEmployee.Birthday)
                );
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
    }
    
}