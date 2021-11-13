using LibraryRenewal.Common.Models;
using LibraryRenewal.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRenewal.Testing.Mocks
{
    class MockJournalRepository : IJournalRepository
    {
        List<Journal> journals = new List<Journal>();
        public async Task AddJournal(Journal j1)
        {
            j1.ItemID = MockGeneralRepository.GetID();
            journals.Add(j1);
        }

        public async Task DeleteJournal(Journal j1)
        {
            for (int i = 0; i < journals.Count; i++)
            {
                if (journals[i].ItemID.Equals(j1.ItemID))
                {
                    journals.RemoveAt(i);
                    return;
                }
            }
        }

        public async Task<List<Journal>> GetAllJournals()
        {
            return journals;
        }

        public async Task<Journal> GetJournal(int id)
        {
            for (int i = 0; i < journals.Count; i++)
            {
                if (journals[i].ItemID.Equals(id))
                {
                    return journals[i];
                }
            }
            return null;
        }

        public async Task UpdateJournal(int id, Journal updatedJournal)
        {
            for (int i = 0; i < journals.Count; i++)
            {
                if (journals[i].ItemID.Equals(id))
                {
                    updatedJournal.ItemID = id;
                    journals[i] = updatedJournal;
                    return;
                }
            }
        }
    }
}
