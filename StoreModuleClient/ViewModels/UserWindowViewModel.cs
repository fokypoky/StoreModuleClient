using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using StoreModuleClient.Models;
using StoreModuleClient.Models.Repositories;
using StoreModuleClient.ViewModels.Base;
using StoreModuleClient.Views.UserControls;

namespace StoreModuleClient.ViewModels;

public class UserWindowViewModel : ViewModel
{
    private ObservableCollection<ProductsView> _productsViews;
    private UserControl _caseQuerryUserControl;
    private UserControl _correlatedQueriesUserControl;
    private UserControl _groupByHavingUserControl;
    private UserControl _nestedQueriesUserControl;    
    
    private UserControl _currentControl = new CaseQuerryUserControl();
    
    public UserControl CurrentControl
    {
        get => _currentControl;
        set => SetField(ref _currentControl, value);
    }
    public ObservableCollection<ProductsView> ProductsViews
    {
        get => _productsViews;
        set => SetField(ref _productsViews, value);
    }

    public ICommand SelectCaseQueryControl
    {
        get => new RelayCommand(() => { CurrentControl = _caseQuerryUserControl; });
    }

    public ICommand SelectCorrelatedQueryControl
    {
        get => new RelayCommand(() => { CurrentControl = _correlatedQueriesUserControl; });
    }

    public ICommand SelectGroupByHavingControl
    {
        get => new RelayCommand(() => { CurrentControl = _groupByHavingUserControl; });
    }

    public ICommand SelectNestedQueriesControl
    {
        get => new RelayCommand(() => { CurrentControl = _nestedQueriesUserControl; });
    }
    public UserWindowViewModel()
    {
        using (var context = new ApplicationContext())
        {
            ProductsViews = new ObservableCollection<ProductsView>(new ProductsViewRepository(context).GetAll());
        }

        _caseQuerryUserControl = new CaseQuerryUserControl();
        _correlatedQueriesUserControl = new CorrelatedQueriesUserControl();
        _groupByHavingUserControl = new GroupByHavingUserControl();
        _nestedQueriesUserControl = new NestedQueriesUserControl();

        _currentControl = _caseQuerryUserControl;
    }
}