using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryRenewal.DAL.Interfaces.Converters
{
    public interface IBookConverter
    {
        Common.Models.Book BookDTOToBook(Models.AbstractItem book);
        Models.AbstractItem BookToBookDTO(Common.Models.Book book);
    }
}
