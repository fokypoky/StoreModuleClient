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

public class AddProductWindowViewModel : ViewModel
{
    private string _productTitle;
    private string _serialNumber;
    private string _cost;
    private List<Manufacturer> _manufacturers;
    private Manufacturer? _selectedManufacturer;

    public Manufacturer? SelectedManufacturer
    {
        get => _selectedManufacturer;
        set => SetField(ref _selectedManufacturer, value);
    }

    public List<Manufacturer> Manufacturers
    {
        get => _manufacturers;
        set => SetField(ref _manufacturers, value);
    }
    public string ProductTitle
    {
        get => _productTitle;
        set => SetField(ref _productTitle, value);
    }

    public string SerialNumber
    {
        get => _serialNumber;
        set => SetField(ref _serialNumber, value);
    }

    public string Cost
    {
        get => _cost;
        set => SetField(ref _cost, value);
    }

    public ICommand AddProductButtonClick
    {
        get => new RelayCommand(AddProduct);
    }

    private void AddProduct()
    {
        if (String.IsNullOrEmpty(ProductTitle) || String.IsNullOrEmpty(SerialNumber)
                                               || String.IsNullOrEmpty(Cost) || SelectedManufacturer == null)
        {
            MessageBox.Show("One or more parameters are empty!");
            return;
        }

        int cost;
        try
        {
            cost = Convert.ToInt32(Cost);
        }
        catch
        {
            MessageBox.Show("Cost parameter contains chars");
            return;
        }

        Product product = new Product()
        {
            ProductTitle = this.ProductTitle,
            SerialNumber = this.SerialNumber,
            Cost = cost,
            Manufacturer = new Manufacturer{ Title = SelectedManufacturer.Title }
        };
        using (var context = new ApplicationContext())
        {
            (new ProductRepository(context)).Add(product);
        }

        MessageBox.Show("OK");
    }
    public AddProductWindowViewModel()
    {
        using (var context = new ApplicationContext())
        {
            Manufacturers = (new ManufacturerRepository(context)).GetAll().ToList();
        }
    }
}