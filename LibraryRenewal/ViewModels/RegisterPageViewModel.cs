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
    public class RegisterPageViewModel : ViewModelBase
    {
        public string TxtUsername { get; set; }
        public string TxtPassword { get; set; }
        public string TxtFullname { get; set; }
        private string _txtPhone;
        public string TxtPhoneNumber
        {
            get { return _txtPhone; }
            set
            {
                if (value.Length > 10||!value.All(char.IsDigit))
                    _txtPhone = "";
                else
                {
                    _txtPhone = value;
                }
            }
        }
        public string TxtError { get; set; }
        private bool _isManager;
        public bool IsManager
        {
            get { return _isManager; }
            set
            {
                _isManager = value;
                _isEmployee = !value;
            }
        }
        private bool _isEmployee;
        public bool IsEmployee
        {
            get { return _isEmployee; }
            set
            {
                _isEmployee = value;
                _isManager = !value;
            }
        }
        public RelayCommand RegisterCommand { get; set; }
        public RelayCommand ReturnCommand { get; set; }
        NavigationData _navData;
        INavigationService _navigationService;
        public RegisterPageViewModel(NavigationData navigationData, INavigationService service)
        {
            _navData = navigationData;
            _navigationService = service;
            IsEmployee = true;
            RegisterCommand = new RelayCommand(Register);
            ReturnCommand = new RelayCommand(Return);
        }
        private async void Register()
        {
            UserType type = UserType.Employee;
            if (IsEmployee)
            {
                type = UserType.Employee;
            }
            else if (IsManager)
            {
                type = UserType.Manager;
            }
            try
            {
                await _navData.LibraryLogic.AddNewUser(TxtUsername, TxtPassword, TxtFullname, TxtPhoneNumber, type);
                _navigationService.NavigateTo("ManagerMainPage");
            }
            catch (BLLUserException ex)
            {

                TxtError = ex.Message;
                ClearAll();
            }
            catch (Exception)
            {
                TxtError = "Unknown error try again or call a manager";
                ClearAll();
            }
        }
        private void Return()
        {
            _navigationService.NavigateTo("ManagerMainPage");
        }
        private void ClearAll()
        {
            TxtUsername = "";
            TxtPassword = "";
            TxtFullname = "";
            TxtPhoneNumber = "";
        }
    }
}
