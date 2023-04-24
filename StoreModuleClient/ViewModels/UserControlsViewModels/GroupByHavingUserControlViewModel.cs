using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StoreModuleClient.Models;
using StoreModuleClient.ViewModels.Base;

namespace StoreModuleClient.ViewModels.UserControlsViewModels;

public class GroupByHavingUserControlViewModel : ViewModel
{
    private List<EmployeeProductCountView> _employeeProducts;
//
    public List<EmployeeProductCountView> EmployeeProducts
    {
        get => _employeeProducts;
        set => SetField(ref _employeeProducts, value);
    }

    public GroupByHavingUserControlViewModel()
    {
        using (var db = new ApplicationContext())
        {
            EmployeeProducts = db.EmployeeProductCountViews.ToList();
        }
    }
}