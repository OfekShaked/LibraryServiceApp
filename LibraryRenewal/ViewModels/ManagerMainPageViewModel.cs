using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using LibraryRenewal.Common.Enums;
using LibraryRenewed.Design.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRenewal.ViewModels
{
    public class ManagerMainPageViewModel : ViewModelBase
    {
        NavigationData _navData;
        INavigationService _navigationService;
        public string UsernameConnected { get; set; }
        public RelayCommand LogoutCommand { get; set; }
        public RelayCommand RegisterNewCommand { get; set; }
        public RelayCommand BookActionsCommand { get; set; }
        public ManagerMainPageViewModel(NavigationData navData, INavigationService navService)
        {
            _navData = navData;
            _navigationService = navService;
            LogoutCommand = new RelayCommand(Logout);
            RegisterNewCommand = new RelayCommand(RegisterNewPage);
            BookActionsCommand = new RelayCommand(BookActionsPage);
        }

        private void BookActionsPage()
        {
            _navigationService.NavigateTo("DisplayBookOptionsPage");
        }

        private void RegisterNewPage()
        {
            _navigationService.NavigateTo("RegisterPage");
        }

        private void Logout()
        {
            _navData.UserType = UserType.Null;
            _navData.UsernameConnected = "";
            _navigationService.NavigateTo("MainPage");
        }
    }
}
