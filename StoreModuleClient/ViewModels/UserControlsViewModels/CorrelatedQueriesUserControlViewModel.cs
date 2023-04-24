using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StoreModuleClient.Models;
using StoreModuleClient.ViewModels.Base;

namespace StoreModuleClient.ViewModels.UserControlsViewModels;

public class CorrelatedQueriesUserControlViewModel : ViewModel
{
    private List<Product> _productsWithSingleAmount;
    private List<Store> _storesWhereAmountMoreThanFive;
    private List<Employee> _employeesHavingMoreThanThreeProductsInStore;

    public List<Employee> EmployeesHavingMoreThanThreeProductsInStore
    {
        get => _employeesHavingMoreThanThreeProductsInStore;
        set => SetField(ref _employeesHavingMoreThanThreeProductsInStore, value);
    }
    public List<Store> StoresWhereAmountMoreThanFive
    {
        get => _storesWhereAmountMoreThanFive;
        set => SetField(ref _storesWhereAmountMoreThanFive, value);
    }
    public List<Product> ProductsWithSingleAmount
    {
        get => _productsWithSingleAmount;
        set => SetField(ref _productsWithSingleAmount, value);
    }

    public CorrelatedQueriesUserControlViewModel()
    {
        using (var context = new ApplicationContext())
        {
            ProductsWithSingleAmount = context
                .Products
                .FromSqlRaw("SELECT * FROM Products p WHERE Cost = ANY(SELECT AllCost FROM Store s WHERE s.ProductId = p.ID)")
                .ToList();
            StoresWhereAmountMoreThanFive = context
                .Stores
                .FromSqlRaw(
                    "SELECT * FROM Store s WHERE EmployeeId IN (SELECT ID FROM Employees e WHERE e.ID = s.EmployeeId) AND Amount > 5;")
                .ToList();
            EmployeesHavingMoreThanThreeProductsInStore = context
                .Employees
                .FromSqlRaw(
                    "SELECT * FROM Employees e WHERE ID IN (SELECT EmployeeId FROM Store s WHERE s.EmployeeId = e.ID AND Amount > 3 )")
                .ToList();
        }
    }
}