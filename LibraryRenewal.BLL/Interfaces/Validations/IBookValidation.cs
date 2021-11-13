using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRenewal.BLL.Interfaces.Validations
{
    public interface IBookValidation
    {
        bool IsBookValid(string name, string writer, string printdate, string publisher, string genre, string discount, string quantity, string price, string isbn, string edition, string summary, out int discountToAdd, out int quantityToAdd, out int priceToAdd,out DateTime date);
    }
}
