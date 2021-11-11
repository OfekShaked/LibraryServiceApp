using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using LibraryRenewal.Views;
using LibraryRenewed.Design.Models;
using LibraryRenewal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRenewal.ViewModels
{
    public class ViewModelLocator
    {
        public DisplayBooksOptionsPageViewModel DisplayBooksOptionsPage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<DisplayBooksOptionsPageViewModel>();
            }
        }
        public MainPageViewModel MainPage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainPageViewModel>();
            }
        }
        public ManagerMainPageViewModel ManagerMainPage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ManagerMainPageViewModel>();
            }
        }
        public RegisterPageViewModel RegisterPage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<RegisterPageViewModel>();
            }
        }
        public ReportPageViewModel ReportPage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ReportPageViewModel>();
            }
        }
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<DisplayBooksOptionsPageViewModel>();
            SimpleIoc.Default.Register<MainPageViewModel>();
            SimpleIoc.Default.Register<ManagerMainPageViewModel>();
            SimpleIoc.Default.Register<RegisterPageViewModel>();
            SimpleIoc.Default.Register<ReportPageViewModel>();
            SimpleIoc.Default.Register<NavigationData>();
            var nav = new NavigationService();
            nav.Configure("ManagerMainPage", typeof(ManagerMainPage));
            nav.Configure("MainPage", typeof(MainPage));
            nav.Configure("DisplayBookOptionsPage", typeof(DisplayBookOptionsPage));
            nav.Configure("RegisterPage", typeof(RegisterPage));
            nav.Configure("ReportPage", typeof(ReportPage));

            SimpleIoc.Default.Register<INavigationService>(() => nav);

        }
    }
}
