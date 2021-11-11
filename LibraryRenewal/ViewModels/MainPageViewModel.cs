using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using LibraryRenewal.BLL.Exceptions;
using LibraryRenewal.Common.Enums;
using LibraryRenewed.Design.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRenewal.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        NavigationData _navData;
        INavigationService _navigationService;
        public MainPageViewModel(NavigationData navigationData,INavigationService service)
        {
            _navData = navigationData;
            _navigationService = service;
            LoginCommand = new RelayCommand(Login);
        }
        public string UsernameBox { get; set; }
        public string PasswordBox { get; set; }
        public string ErrorBox { get; set; }
        public RelayCommand LoginCommand { get; private set; }
        public async void Login()
        {
            UserType userConnected=UserType.Null;
            try
            {
                userConnected = await _navData.LibraryLogic.VerifyUser(UsernameBox, PasswordBox);
            }
            catch(BLLUserException ex)
            {
                ErrorBox = ex.Message;
            }
            if (userConnected == UserType.Null)
            {
                ErrorBox = "Incorrect Username/Password";
                _navigationService.NavigateTo("ManagerMainPage");
            }
            else if (userConnected == UserType.Manager)
            {
                _navData.UserType = userConnected;
                _navData.UsernameConnected = UsernameBox;
                _navigationService.NavigateTo("ManagerMainPage");
            }else if (userConnected == UserType.Employee)
            {
                _navData.UserType = userConnected;
                _navData.UsernameConnected = UsernameBox;
                _navigationService.NavigateTo("DisplayBookOptionsPage");
            }
        }
    }
}
