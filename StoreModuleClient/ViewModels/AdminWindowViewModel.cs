using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using StoreModuleClient.ViewModels.Base;
using StoreModuleClient.Models;
using StoreModuleClient.Models.Repositories;
using StoreModuleClient.Views.CRUD;

namespace StoreModuleClient.ViewModels;

public class AdminWindowViewModel : ViewModel
{
    #region PROPERTIES

    private ObservableCollection<Employee> _employees;
    private ObservableCollection<Manufacturer> _manufacturers;
    private ObservableCollection<Product> _products;
    private ObservableCollection<Store> _stores;
    private string _newManufacturerTitle;
    private string _newProductCost;
    private string _newStoreAmount;
    private string _newEmployeeName;
    private string _newEmployeePassport;
    private DateTime _newEmployeeBirthday;
    
    private Employee? _selectedEmployee = null;
    private Manufacturer? _selectedManufacturer = null;
    private Product? _selectedProduct = null;
    private Store? _selectedStore = null;

    public string NewEmployeeName
    {
        get => _newEmployeeName;
        set => SetField(ref _newEmployeeName, value);
    }

    public string NewEmployeePassport
    {
        get => _newEmployeePassport;
        set => SetField(ref _newEmployeePassport, value);
    }

    public DateTime NewEmployeeBirthday
    {
        get => _newEmployeeBirthday;
        set => SetField(ref _newEmployeeBirthday, value);
    }
    
    public string NewStoreAmount
    {
        get => _newStoreAmount;
        set => SetField(ref _newStoreAmount, value);
    }
    public string NewProductCost
    {
        get => _newProductCost;
        set => SetField(ref _newProductCost, value);
    }
    
    public string NewManufacturerTitle
    {
        get => _newManufacturerTitle;
        set => SetField(ref _newManufacturerTitle, value);
    }
    
    public ObservableCollection<Employee> Employees
    {
        get => _employees;
        set => SetField(ref _employees, value);
    }

    public ObservableCollection<Manufacturer> Manufacturers
    {
        get => _manufacturers;
        set => SetField(ref _manufacturers, value);
    }

    public ObservableCollection<Product> Products
    {
        get => _products;
        set => SetField(ref _products, value);
    }

    public ObservableCollection<Store> Stores
    {
        get => _stores;
        set => SetField(ref _stores, value);
    }
    public Employee? SelectedEmployee
    {
        get => _selectedEmployee;
        set => SetField(ref _selectedEmployee, value);
    }

    public Manufacturer? SelectedManufacturer
    {
        get => _selectedManufacturer;
        set => SetField(ref _selectedManufacturer, value);
    }

    public Product? SelectedProduct
    {
        get => _selectedProduct;
        set => SetField(ref _selectedProduct, value);
    }

    public Store? SelectedStore
    {
        get => _selectedStore;
        set => SetField(ref _selectedStore, value);
    }


    #endregion

    #region COMMANDS

    public ICommand UpdateEmployeeButtonClick
    {
        get => new RelayCommand(UpdateEmployee);
    }
    private void UpdateEmployee()
    {
        if (SelectedEmployee == null)
        {
            MessageBox.Show("You didn't select employee");
            return;
        }

        if (String.IsNullOrEmpty(NewEmployeeName) || String.IsNullOrEmpty(NewEmployeePassport))
        {
            MessageBox.Show("One or more parameters are empty");
            return;
        }

        var newEmployee = new Employee()
        {
            EmployeeName = NewEmployeeName,
            Birthday = new DateOnly(
                year: NewEmployeeBirthday.Year,
                month: NewEmployeeBirthday.Month,
                day: NewEmployeeBirthday.Day
            ),
            Passport = NewEmployeePassport
        };
        using (var context = new ApplicationContext())
        {
            new EmployeeRepository(context).Update(SelectedEmployee, newEmployee);
            MessageBox.Show("OK");
        }
    }
    public ICommand RemoveEmployeeButtonClick
    {
        get => new RelayCommand(RemoveEmployee);
    }

    private void RemoveEmployee()
    {
        if (SelectedEmployee == null)
        {
            MessageBox.Show("You didn't select employee");
            return;
        }

        using (var context = new ApplicationContext())
        {
            new EmployeeRepository(context).Remove(SelectedEmployee);
            MessageBox.Show("OK");
        }
    }
    public ICommand AddEmployeeButtonClick
    {
        get => new RelayCommand(AddEmployee);
    }
    private void AddEmployee()
    {
        AddEmployeeWindow window = new AddEmployeeWindow();
        window.Show();
    }
    public ICommand UpdateEmployeesListButtonClick
    {
        get => new RelayCommand(UpdateEmployees);
    }
    public ICommand UpdateProductAmountInStoreButtonClick
    {
        get => new RelayCommand(UpdateProductAmountInStore);
    }

    private void UpdateProductAmountInStore()
    {
        if (String.IsNullOrEmpty(NewStoreAmount))
        {
            MessageBox.Show("New amount is empty");
            return;
        }

        if (SelectedStore == null)
        {
            MessageBox.Show("You didn't select store");
            return;
        }

        int amount;
        try
        {
            amount = Convert.ToInt32(NewStoreAmount);
        }
        catch
        {
            MessageBox.Show("New product amount contains chars");
            return;
        }

        using (var context = new ApplicationContext())
        {
            new StoreRepository(context).Update(SelectedStore, amount);
            MessageBox.Show("OK");
        }
    }
    public ICommand RemoveProductFromStoreButtonClick
    {
        get => new RelayCommand(RemoveProductFromStore);
    }

    private void RemoveProductFromStore()
    {
        if (SelectedStore == null)
        {
            MessageBox.Show("You didn't select product in store");
            return;
        }

        using (var context = new ApplicationContext())
        {
            new StoreRepository(context).Remove(SelectedStore);
            MessageBox.Show("OK");
        }
    }
    public ICommand AddProductToStoreButtonClick
    {
        get => new RelayCommand(AddProductToStore);
    }

    private void AddProductToStore()
    {
        var window = new AddProductToStoreWindow();
        window.Show();
    }
    
    public ICommand UpdateStoreListButtonClick
    {
        get => new RelayCommand(UpdateStores);
    }
    public ICommand UpdateProductCostButtonClick
    {
        get => new RelayCommand(UpdateProductCost);
    }

    private void UpdateProductCost()
    {
        if (SelectedProduct == null)
        {
            MessageBox.Show("You didn't select product");
            return;
        }

        if (String.IsNullOrEmpty(NewProductCost))
        {
            MessageBox.Show("New cost parameter is empty");
            return;
        }
        int cost;
        try
        {
            cost = Convert.ToInt32(NewProductCost);
        }
        catch
        {
            MessageBox.Show("Cost can't contains symbols");
            return;
        }

        using (var context = new ApplicationContext())
        {
            (new ProductRepository(context)).Update(SelectedProduct, cost);
        }

        MessageBox.Show("OK");
    }
    public ICommand UpdateProductListButtonClick
    {
        get => new RelayCommand(UpdateProducts);
    }
    public ICommand RemoveProductButtonClick
    {
        get => new RelayCommand(RemoveProduct);
    }

    private void RemoveProduct()
    {
        if (SelectedProduct == null)
        {
            MessageBox.Show("You didn't select product!");
            return;
        }

        using (var context = new ApplicationContext())
        {
            (new ProductRepository(context)).Remove(SelectedProduct);
            MessageBox.Show($"OK");
        }
    }
    public ICommand AddProductButtonClick
    {
        get => new RelayCommand(AddProduct);
    }
    private void AddProduct()
    {
        AddProductWindow addProductWindow = new AddProductWindow();
        addProductWindow.Show();
    }
    public ICommand AddManufacturerButtonClick
    {
        get => new RelayCommand(AddManufacturer);
    }
    private void AddManufacturer()
    {
        var addManufacturerWindow = new AddManufacturerWindow();
        addManufacturerWindow.Show();
    }
    public ICommand RemoveManufacturerButtonClick
    {
        get => new RelayCommand(RemoveManufacturer);
    }
    private void RemoveManufacturer()
    {
        if (SelectedManufacturer == null)
        {
            MessageBox.Show("You didn't select manufacturer!");
            return;
        }
        using (var context = new ApplicationContext())
        {
            new ManufacturerRepository(context).Remove(SelectedManufacturer);
        }
        UpdateManufacturers();
    }
    public ICommand UpdateManufacturersListButtonClick
    {
        get => new RelayCommand(UpdateManufacturers);
    }
    public ICommand UpdateManufacturerButtonClick
    {
        get => new RelayCommand(UpdateManufacturer);
    }
    private void UpdateManufacturer()
    {
        if (SelectedManufacturer == null)
        {
            MessageBox.Show("Manufacturer is null");
            return;
        }

        if (String.IsNullOrEmpty(NewManufacturerTitle))
        {
            MessageBox.Show("New manufacturer title is empty");
            return;
        }

        using (var context = new ApplicationContext())
        {
            new ManufacturerRepository(context).Update(SelectedManufacturer.Title, NewManufacturerTitle);
        }

        MessageBox.Show("OK");
    }
    #endregion
    
    public AdminWindowViewModel()
    {
        UpdateAll();
    }
    private void UpdateAll()
    {
        UpdateEmployees();
        UpdateManufacturers();
        UpdateProducts();
        UpdateStores();
    }
    private void UpdateStores()
    {
        using (var context = new ApplicationContext())
        {
            Stores?.Clear();
            Stores = new ObservableCollection<Store>(new StoreRepository(context).GetAll());           
        }
    }
    private void UpdateProducts()
    {
        using (var context = new ApplicationContext())
        {
            Products?.Clear();
            Products = new ObservableCollection<Product>(new ProductRepository(context).GetAll());   
        }
    }
    private void UpdateManufacturers()
    {
        using (var context = new ApplicationContext())
        {
            Manufacturers?.Clear();
            Manufacturers = new ObservableCollection<Manufacturer>(new ManufacturerRepository(context).GetAll());   
        }
    }
    private void UpdateEmployees()
    {
        using (var context = new ApplicationContext())
        {
            Employees?.Clear();
            Employees = new ObservableCollection<Employee>(new EmployeeRepository(context).GetAll());   
        }
    }
}