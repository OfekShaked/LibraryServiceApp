using LibraryRenewal.BLL.Interfaces.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRenewal.BLL.Validations
{
    public class JournalValidation : IJournalValidations
    {
        IGeneralValidations _generalValidations;
        public JournalValidation(IGeneralValidations generalValidations)
        {
            _generalValidations = generalValidations;
        }
        public bool IsJournalValid(string name, string writer, string printdate, string publisher, string genre, string discount, string quantity, string price, string subject, out int priceToAdd, out int discountToAdd, out int quantityToAdd,out DateTime date)
        {
            date = _generalValidations.IsDateValid(printdate);
            discountToAdd = _generalValidations.IsDiscountValid(discount);
            quantityToAdd = _generalValidations.IsQuantityValid(quantity);
            priceToAdd = _generalValidations.IsPriceValid(price);
            return true;
        }
    }
}
