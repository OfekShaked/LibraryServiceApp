using LibraryRenewal.BLL.Interfaces;
using LibraryRenewal.BLL.Services;
using LibraryRenewal.Common.Enums;
using LibraryRenewal.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRenewed.Design.Models
{
    public class NavigationData
    {
        public NavigationData()
        {
            LibraryLogic = new GeneralLibraryLogic();
        }
        public IGeneralLibraryLogic LibraryLogic { get; set; }
        public UserType UserType { get; set; }
        public List<AbstractItem> searchResult { get; set; }
        public string UsernameConnected { get; set; }
        private static readonly object padlock = new object();
        private static NavigationData _pageData = null;
        public static NavigationData PageData
        {
            get
            {
                if (_pageData == null)
                {
                    lock (padlock)
                    {
                        if (_pageData == null)
                            _pageData = new NavigationData();
                    }
                }
                return _pageData;
            }
        }
    }
}
