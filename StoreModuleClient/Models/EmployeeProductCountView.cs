using System;

namespace StoreModuleClient.Models;

public class EmployeeProductCountView
{
    public string Employee { get; set; }
    public DateOnly Birthday { get; set; }
    public int Count { get; set; }
}