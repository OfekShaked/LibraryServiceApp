using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRenewed.Design.ViewModels
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

        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<DisplayBooksOptionsPageViewModel>();
            SimpleIoc.Default.Register<MainPageViewModel>();
            SimpleIoc.Default.Register<ManagerMainPageViewModel>();
            SimpleIoc.Default.Register<RegisterPageViewModel>();
            SimpleIoc.Default.Register<ReportPageViewModel>();

        }
    }
}
