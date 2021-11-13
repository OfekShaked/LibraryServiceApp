using LibraryRenewal.BLL.Exceptions;
using LibraryRenewal.BLL.Interfaces.Validations;
using LibraryRenewal.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRenewal.BLL.Validations
{
    public class BookValidations : IBookValidation
    {
        IGeneralValidations _generalValidations;
        public BookValidations(IGeneralValidations generalValidations)
        {
            _generalValidations = generalValidations;
        }
        public bool IsBookValid(string name, string writer, string printdate, string publisher, string genre, string discount, string quantity, string price, string isbn, string edition, string summary,out int discountToAdd, out int quantityToAdd, out int priceToAdd,out DateTime date)
        {
            if (name == "" || writer == "" || printdate == "" || printdate == "" || publisher == "" || genre == "" || discount == "" || quantity == "" || price == "" || isbn == "" || edition == "" || summary == "")
            {
                Task.Run(() => GeneralLibraryLogic.SaveToLogFile("All book fields must be full in order to add a new book!")); 
                throw new BLLBookException("All book fields must be full in order to add a new book!");
            }
            date = _generalValidations.IsDateValid(printdate);
            discountToAdd = _generalValidations.IsDiscountValid(discount);
            quantityToAdd = _generalValidations.IsQuantityValid(quantity);
            priceToAdd = _generalValidations.IsPriceValid(price);
            return true;
        }
    }
}
