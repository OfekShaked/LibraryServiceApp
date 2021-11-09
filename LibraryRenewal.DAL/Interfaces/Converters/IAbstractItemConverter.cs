using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryRenewal.DAL.Interfaces.Converters
{
    public interface IAbstractItemConverter
    {
        Common.Models.AbstractItem AbstractItemDTOToAbstractItem(Models.AbstractItem abstractItem);
        Models.AbstractItem AbstractItemToAbstractItemDTO(Common.Models.AbstractItem abstractItem);
    }
}
