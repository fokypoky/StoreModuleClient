using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using StoreModuleClient.Models;
using StoreModuleClient.ViewModels.Base;
using StoreModuleClient.Views;

namespace StoreModuleClient.ViewModels;

public class MainWindowViewModel : ViewModel
{
    private string _login;
    private string _password;

    public string Login
    {
        get => _login;
        set => SetField(ref _login, value);
    }

    public string Password
    {
        get => _password;
        set => SetField(ref _password, value);
    }

    public ICommand LoginCommand
    {
        get => new RelayCommand(OnLoginCommandExecuted);
    }
    
    private void OnLoginCommandExecuted()
    {
        switch (GetRole())
        {
            case Role.Admin:
                ApplicationContext.ConnectionString =
                    "Server=DESKTOP-VJDHRQC;Database=StoreModuleDB;User id=admin; Password=admin;TrustServerCertificate=True";
                AdminWindow adminWindow = new AdminWindow();
                adminWindow.Show();
                break;
            case Role.User:
                ApplicationContext.ConnectionString = 
                    "Server=DESKTOP-VJDHRQC;Database=StoreModuleDB;User id=user; Password=;TrustServerCertificate=True";
                UserWindow userWindow = new UserWindow();
                userWindow.Show();
                break;
            default:
                MessageBox.Show("This user not exists");
                break;
        }
    }

    private Role GetRole()
    {
        try
        {
            string connectionString = @"Data Source=DESKTOP-VJDHRQC;Initial Catalog=StoreModuleDB;User id=" + Login +
                                      ";Password=" + Password + ";";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand sqlCommand = new SqlCommand($"SELECT dbo.WHOAMI()", connection);
            SqlDataAdapter adapter = new SqlDataAdapter();

            DataTable table = new DataTable();

            adapter.SelectCommand = sqlCommand;
            adapter.Fill(table);

            string role = table.Rows[0][0].ToString();
            
            connection.Close();

            if (role == "1")
            {
                return Role.Admin;
            }
            if (role == "2")
            {
                return Role.User;
            }

            return Role.None;
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
            return Role.None;
        }
    }
}