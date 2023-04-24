using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using StoreModuleClient.Models;
using StoreModuleClient.Models.Repositories;
using StoreModuleClient.ViewModels.Base;

namespace StoreModuleClient.ViewModels.CRUD;

public class AddProductToStoreWindowViewModel : ViewModel
{
    private List<Product> _products;
    private List<Employee> _employees;
    private Product? _selectedProduct;
    private Employee? _selectedEmployee;
    private string _productAmount;

    public string ProductAmount
    {
        get => _productAmount;
        set => SetField(ref _productAmount, value);
    }
    public Product? SelectedProduct
    {
        get => _selectedProduct;
        set => SetField(ref _selectedProduct, value);
    }

    public Employee? SelectedEmployee
    {
        get => _selectedEmployee;
        set => SetField(ref _selectedEmployee, value);
    }
    public List<Product> Products
    {
        get => _products;
        set => SetField(ref _products, value);
    }

    public List<Employee> Employees
    {
        get => _employees;
        set => SetField(ref _employees, value);
    }

    public ICommand AddButtonClick
    {
        get => new RelayCommand(AddProductToStore);
    }

    private void AddProductToStore()
    {
        if (String.IsNullOrEmpty(ProductAmount) || SelectedEmployee == null || SelectedProduct == null)
        {
            MessageBox.Show("One or more parameters are empty");
            return;
        }

        int amount;
        try
        {
            amount = Convert.ToInt32(ProductAmount);
        }
        catch
        {
            MessageBox.Show("Product amount can't contain chars");
            return;
        }

        Store store = new Store()
        {
            Product = SelectedProduct,
            Employee = SelectedEmployee,
            Amount = amount
        };
        using (var context = new ApplicationContext())
        {
            new StoreRepository(context).Add(store);
            MessageBox.Show("OK");
        }
    }
    public AddProductToStoreWindowViewModel()
    {
        using (var context = new ApplicationContext())
        {
            Employees = (new EmployeeRepository(context)).GetAll().ToList();
            Products = (new ProductRepository(context)).GetAll().ToList();
        }
    }
}