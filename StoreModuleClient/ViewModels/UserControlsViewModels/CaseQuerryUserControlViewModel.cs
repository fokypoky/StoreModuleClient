using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using StoreModuleClient.Models;
using StoreModuleClient.Models.Repositories;
using StoreModuleClient.ViewModels.Base;

namespace StoreModuleClient.ViewModels.UserControlsViewModels;

public class CaseQuerryUserControlViewModel : ViewModel
{
    private List<PersonPassportsByAmount> _passports;

    public List<PersonPassportsByAmount> Passports
    {
        get => _passports;
    }
    public CaseQuerryUserControlViewModel()
    {
        using (var context = new ApplicationContext())
        {
            _passports = context.Set<PersonPassportsByAmount>()
                .FromSqlRaw("SELECT * FROM dbo.GetEmployeesPassportByProductAmount(2)")
                .ToList();
        }
    }
}