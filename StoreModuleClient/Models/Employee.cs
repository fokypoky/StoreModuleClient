using System;
using System.Collections.Generic;

namespace StoreModuleClient.Models;

public partial class Employee
{
    public int Id { get; set; }
    public string EmployeeName { get; set; } = null!;

    public string Passport { get; set; } = null!;

    public DateOnly Birthday { get; set; }

    public virtual ICollection<Store> Stores { get; } = new List<Store>();
}
