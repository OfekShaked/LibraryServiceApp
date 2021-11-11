using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using LibraryRenewal.BLL.Exceptions;
using LibraryRenewal.Common.Models;
using LibraryRenewed.Design.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace LibraryRenewal.ViewModels
{
    public class ReportPageViewModel : ViewModelBase
    {
        public NavigationData NavData { get; private set; }
        INavigationService _navService;
        public ObservableCollection<AbstractItem> Items { get; set; }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand ReturnCommand { get; set; }

        private string _txtDiscount;
        public string TxtDiscount
        {
            get { return _txtDiscount; }
            set
            {
                if (!value.All(char.IsDigit))
                    _txtDiscount = "";
                else
                {
                    _txtDiscount = value;
                }
            }
        }
        public ReportPageViewModel(NavigationData navData, INavigationService navigationService)
        {
            NavData = navData;
            _navService = navigationService;
            AddCommand = new RelayCommand(AddDiscount);
            ReturnCommand = new RelayCommand(Return);
            Items = new ObservableCollection<AbstractItem>(NavData.searchResult);
        }
        private async void AddDiscount()
        {
            try
            {
                await NavData.LibraryLogic.AddDiscounts(NavData.searchResult, TxtDiscount);
            }
            catch (BLLGeneralException)
            {
                MessageDialog md = new MessageDialog("Cannot add discounts atm try again later");
                await md.ShowAsync();
                return;
            }
            NavData.searchResult = null;
            _navService.NavigateTo("ManagerMainPage");
        }
        private void Return()
        {
            _navService.NavigateTo("ManagerMainPage");
        }
    }
}
