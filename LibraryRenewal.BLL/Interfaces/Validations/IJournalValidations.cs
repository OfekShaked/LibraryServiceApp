using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRenewal.BLL.Interfaces.Validations
{
    public interface IJournalValidations
    {
        bool IsJournalValid(string name, string writer, string printdate, string publisher, string genre, string discount, string quantity, string price, string subject,out int priceToAdd,out int discountToAdd,out int quantityToAdd, out DateTime date);
    }
}
