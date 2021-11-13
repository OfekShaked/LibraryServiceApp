using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRenewal.BLL.Interfaces.Validations
{
    public interface IGeneralValidations
    {
        int IsItemIdValid(string itemID);
        DateTime IsDateValid(string date);
        int IsDiscountValid(string discount);
        int IsQuantityValid(string quantity);
        int IsPriceValid(string price);
    }
}
