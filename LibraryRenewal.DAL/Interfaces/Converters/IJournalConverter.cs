using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryRenewal.DAL.Interfaces.Converters
{
    public interface IJournalConverter
    {
        Models.AbstractItem JournalToJournalDTO(Common.Models.Journal journal);
        Common.Models.Journal JournalDTOToJournal(Models.AbstractItem journal);
    }
}
