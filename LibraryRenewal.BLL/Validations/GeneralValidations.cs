using LibraryRenewal.BLL.Exceptions;
using LibraryRenewal.BLL.Interfaces.Validations;
using LibraryRenewal.BLL.Services;
using LibraryRenewal.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRenewal.BLL.Validations
{
    public class GeneralValidations : IGeneralValidations
    {

        public int IsItemIdValid(string itemID)
        {
            int id;
            if (int.TryParse(itemID, out id) == false)
            {
                Task.Run(()=> GeneralLibraryLogic.SaveToLogFile("Id is not valid cannot convert to int"));
                throw new BLLBookException("Id is not valid");
            }
            return id;
        }
        public DateTime IsDateValid(string printDate)
        {
            DateTime date;
            if (DateTime.TryParse(printDate, out date) == false)
            {
                Task.Run(() => GeneralLibraryLogic.SaveToLogFile("Cannot Convert date to DateTime!"));
                throw new BLLBookException("Date Time Not in a correct format");
            }
            return date;
        }

        public int IsDiscountValid(string discount)
        {
            int discountToAdd;
            if (int.TryParse(discount, out discountToAdd) == false)
            {
                Task.Run(() => GeneralLibraryLogic.SaveToLogFile("discount is not a number!"));
                throw new BLLBookException("discount is not a number!");
            }
            if (discountToAdd < 0 || discountToAdd > 99)
            {
                Task.Run(() => GeneralLibraryLogic.SaveToLogFile("Discount must be between 0-99"));
                throw new BLLBookException("Discount must be between 0-99");
            }
            return discountToAdd;
        }

        public int IsQuantityValid(string quantity)
        {
            int quantityToAdd;
            if (int.TryParse(quantity, out quantityToAdd) == false)
            {
                Task.Run(() => GeneralLibraryLogic.SaveToLogFile("quantity is not a number"));
                throw new BLLBookException("quantity is not a number");
            }
            if (quantityToAdd < 0)
            {
                Task.Run(() => GeneralLibraryLogic.SaveToLogFile("Quantity cannot be negative!"));
                throw new BLLBookException("Quantity cannot be negative!");
            }
            return quantityToAdd;
        }

        public int IsPriceValid(string price)
        {
            int priceToAdd;
            if (int.TryParse(price, out priceToAdd) == false)
            {
                Task.Run(() => GeneralLibraryLogic.SaveToLogFile("Price is not a number"));
                throw new BLLBookException("Price is not a number");
            }
            if (priceToAdd < 0)
            {
                Task.Run(() => GeneralLibraryLogic.SaveToLogFile("Price cannot be negative!"));
                throw new BLLBookException("Price cannot be negative!");
            }
            return priceToAdd;
        }
    }
}
