using System;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using StoreModuleClient.Models;
using StoreModuleClient.Models.Repositories;
using StoreModuleClient.ViewModels.Base;

namespace StoreModuleClient.ViewModels.CRUD;

public class AddManufacturerWindowViewModel : ViewModel
{
    private string _title;
    private string _country;

    public string Title
    {
        get => _title;
        set => SetField(ref _title, value);
    }
    
    public string Country
    {
        get => _country;
        set => SetField(ref _country, value);
    }

    public ICommand AddButtonClick
    {
        get => new RelayCommand(AddManufacturer);
    }

    private void AddManufacturer()
    {
        var manufacturer = new Manufacturer()
        {
            Country = this.Country,
            Title = this.Title
        };
        using (var context = new ApplicationContext())
        {
            new ManufacturerRepository(context).Add(manufacturer);
        }
    }
}