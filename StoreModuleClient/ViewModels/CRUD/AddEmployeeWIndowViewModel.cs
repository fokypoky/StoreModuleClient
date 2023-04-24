using System;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using StoreModuleClient.Models;
using StoreModuleClient.Models.Repositories;
using StoreModuleClient.ViewModels.Base;

namespace StoreModuleClient.ViewModels.CRUD;

public class AddEmployeeWIndowViewModel : ViewModel
{
    private string _name;
    private string _passport;
    private DateTime _birthday;

    public string Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }

    public string Passport
    {
        get => _passport;
        set => SetField(ref _passport, value);
    }

    public DateTime Birthday
    {
        get => _birthday;
        set => SetField(ref _birthday, value);
    }

    public ICommand AddButtonClick
    {
        get => new RelayCommand(AddEmployee);
    }

    private void AddEmployee()
    {
        if (String.IsNullOrEmpty(Name) || String.IsNullOrEmpty(Passport))
        {
            MessageBox.Show("One or more parameters are empty");
            return;
        }

        var employee = new Employee()
        {
            EmployeeName = Name,
            Passport = this.Passport
        };
        using (var context = new ApplicationContext())
        {
            new EmployeeRepository(context).Add(employee, Birthday);
            MessageBox.Show("OK");
        }
    }
}