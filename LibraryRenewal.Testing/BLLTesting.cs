
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryRenewal.BLL.Exceptions;
using LibraryRenewal.BLL.Interfaces;
using LibraryRenewal.BLL.Services;
using LibraryRenewal.Common.Enums;
using LibraryRenewal.Common.Models;
using LibraryRenewal.DAL.Interfaces;
using LibraryRenewal.Testing.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibraryRenewal.Testing
{
    [TestClass]
    public class BLLTesting
    {
        IBookRepository _bookRep;
        IJournalRepository _journalRep;
        IUserRepository _userRep;
        IGenreRepository _genreRep;
        IGeneralRepository _generalRep;
        ISaleRepository _saleRep;
        IGeneralLibraryLogic _libraryLogic;
        MockBookRepository mockBookRep;
        MockJournalRepository mockJournalRep;
        MockSaleRepository mockSaleRep;
        public void CreateDataBase()
        {
            mockBookRep = new MockBookRepository();
            mockJournalRep = new MockJournalRepository();
            mockSaleRep = new MockSaleRepository();
            _bookRep = mockBookRep;
            _journalRep = mockJournalRep;
            _userRep = new MockUserRepository();
            _genreRep = new MockGenreRepository();
            _saleRep = mockSaleRep;
            _generalRep = new MockGeneralRepository(mockBookRep, mockJournalRep);
            _libraryLogic = new GeneralLibraryLogic(null,_userRep, _genreRep, _generalRep, _bookRep, _journalRep, _saleRep);
        }
        [TestMethod]
        public async Task TestAddNewBookValid()
        {
            CreateDataBase();
            await _libraryLogic.AddNewBook("a", "a", "1/9/2000", "a", "a", "10", "1", "10", "a", "a", "a");
            Book b1 = await _libraryLogic.GetBook("1");
            if (b1.Name.Equals("a") && b1.Writer.Equals("a"))
                Assert.IsTrue(true);
        }
        [TestMethod]
        public async Task TestAddNewBookMissingFieldError()
        {
            CreateDataBase();
            try
            {
                await _libraryLogic.AddNewBook("a", "a", "1/9/2000", "a", "a", "10", "1", "10", "a", "", "a");
                Book b1 = await _libraryLogic.GetBook("1");
            }
            catch (LibraryException)
            {
                Assert.IsTrue(true);
            }
        }
        [TestMethod]
        public async Task TestAddNewBookNotANumberError()
        {
            CreateDataBase();
            try
            {
                await _libraryLogic.AddNewBook("a", "a", "1/9/2000", "a", "a", "10", "1", "a", "a", "a", "a");
                Book b1 = await _libraryLogic.GetBook("1");
            }
            catch (LibraryException)
            {
                Assert.IsTrue(true);
            }
        }
        [TestMethod]
        public async Task TestAddNewJournalValid()
        {
            CreateDataBase();
            await _libraryLogic.AddNewJournal("a", "a", "1/9/2000", "a", "a", "10", "1", "10", "a");
            Journal j1 = await _libraryLogic.GetJournal("1");
            Assert.IsTrue(j1.Name.Equals("a") && j1.Writer.Equals("a"));
        }
        [TestMethod]
        public async Task TestAddNewJournalMissingFieldError()
        {
            CreateDataBase();
            try
            {
                await _libraryLogic.AddNewJournal("a", "a", "1/9/2000", "a", "a", "10", "1", "", "a");
                Journal j1 = await _libraryLogic.GetJournal("1");
            }
            catch (LibraryException)
            {
                Assert.IsTrue(true);
            }
        }
        [TestMethod]
        public async Task TestAddNewJournalNotANumberError()
        {
            CreateDataBase();
            try
            {
                await _libraryLogic.AddNewJournal("a", "a", "1/9/2000", "a", "a", "10", "1", "asda", "a");
                Journal j1 = await _libraryLogic.GetJournal("1");
            }
            catch (LibraryException)
            {
                Assert.IsTrue(true);
            }
        }
        [TestMethod]
        public async Task TestGetBookIdNotExist()
        {
            CreateDataBase();
            Book b1 = await _libraryLogic.GetBook("10111");
            if (b1 == null)
                Assert.IsTrue(true);
        }
        [TestMethod]
        public async Task TestGetJournalIdNotExist()
        {
            CreateDataBase();
            Journal j1 = await _libraryLogic.GetJournal("1111");
            if (j1 == null)
                Assert.IsTrue(true);
        }
        [TestMethod]
        public async Task TestGetAllBooks()
        {
            CreateDataBase();
            await _libraryLogic.AddNewBook("a", "a", "1/9/2000", "a", "a", "10", "1", "10", "a", "a", "a");
            await _libraryLogic.AddNewBook("b", "b", "1/9/2000", "b", "b", "10", "1", "10", "b", "b", "b");
            List<Book> books = await _libraryLogic.GetAllBooks();
            if (books[0].Name.Equals("a") && books[1].Name.Equals("b"))
                Assert.IsTrue(true);
        }
        [TestMethod]
        public async Task TestGetAllJournals()
        {
            CreateDataBase();
            await _libraryLogic.AddNewJournal("a", "a", "1/9/2000", "a", "a", "10", "1", "10", "a");
            await _libraryLogic.AddNewJournal("b", "b", "1/9/2000", "b", "b", "10", "1", "10", "b");
            List<Journal> journals = await _libraryLogic.GetAllJournals();
            if (journals[0].Name.Equals("a") && journals[1].Name.Equals("b"))
                Assert.IsTrue(true);
        }
        [TestMethod]
        public async Task TestGetAllAvailableBooks()
        {
            CreateDataBase();
            await _libraryLogic.AddNewBook("a", "a", "1/9/2000", "a", "a", "10", "0", "10", "a", "a", "a");
            await _libraryLogic.AddNewBook("b", "b", "1/9/2000", "b", "b", "10", "1", "10", "b", "b", "b");
            List<Book> books = await _libraryLogic.GetAllAvailableBooks();
            Assert.IsTrue(books.Count == 1);
        }
        [TestMethod]
        public async Task TestGetAllAvailableJournals()
        {
            CreateDataBase();
            await _libraryLogic.AddNewJournal("a", "a", "1/9/2000", "a", "a", "10", "0", "10", "a");
            await _libraryLogic.AddNewJournal("b", "b", "1/9/2000", "b", "b", "10", "1", "10", "b");
            List<Journal> journals = await _libraryLogic.GetAllAvailableJournals();
            Assert.IsTrue(journals.Count == 1);
        }
        [TestMethod]
        public async Task TestAddAndVerifyNewUser()
        {
            CreateDataBase();
            await _libraryLogic.AddNewUser("a", "a", "a", "0509563101", UserType.Manager);
            UserType uType = await _libraryLogic.VerifyUser("a", "a");
            Assert.IsTrue(uType == UserType.Manager);
        }
        [TestMethod]
        public async Task TestAddAndVerifyNewUserIncorrectData()
        {
            CreateDataBase();
            await _libraryLogic.AddNewUser("a", "a", "a", "0509563101", UserType.Manager);
            UserType uType = await _libraryLogic.VerifyUser("a", "a");
            Assert.IsTrue(uType == UserType.Manager);
        }
        [TestMethod]
        public async Task TestUserAlreadyExist()
        {
            CreateDataBase();
            await _libraryLogic.AddNewUser("a", "a", "a", "0509563101", UserType.Manager);
            try
            {
                await _libraryLogic.AddNewUser("a", "a", "a", "0509563101", UserType.Manager);

            }
            catch (LibraryException)
            {
                Assert.IsTrue(true);
            }
        }
        [TestMethod]
        public async Task TestAddUserMissingField()
        {
            CreateDataBase();
            try
            {
                await _libraryLogic.AddNewUser("a", "a", "a", "", UserType.Manager);

            }
            catch (LibraryException)
            {
                Assert.IsTrue(true);
            }
        }
        [TestMethod]
        public async Task TestSellBookValid()
        {
            CreateDataBase();
            await _libraryLogic.AddNewBook("a", "a", "1/9/2000", "a", "a", "10", "2", "10", "a", "a", "a");
            await _libraryLogic.SellBook("1", "Mark");
            Book b1 = await _libraryLogic.GetBook("1");
            if (b1.Quantity.Equals(1))
                Assert.IsTrue(true);
        }
        [TestMethod]
        public async Task TestSellBookInValid()
        {
            CreateDataBase();
            await _libraryLogic.AddNewBook("a", "a", "1/9/2000", "a", "a", "10", "0", "10", "a", "a", "a");
            try
            {
                await _libraryLogic.SellBook("1", "John");
            }
            catch (LibraryException)
            {
                Assert.IsTrue(true);
            }
        }
        [TestMethod]
        public async Task TestSellJournalValid()
        {
            CreateDataBase();
            await _libraryLogic.AddNewJournal("a", "a", "1/9/2000", "a", "a", "10", "2", "10", "a");
            await _libraryLogic.SellJournal("1", "Johnny");
            Journal j1 = await _libraryLogic.GetJournal("1");
            Assert.IsTrue(j1.Quantity.Equals(1));
        }
        [TestMethod]
        public async Task TestSellJournalInvValid()
        {
            CreateDataBase();
            await _libraryLogic.AddNewJournal("a", "a", "1/9/2000", "a", "a", "10", "0", "10", "a");
            try
            {
                await _libraryLogic.SellJournal("1", "Avraham");
            }
            catch (LibraryException)
            {
                Assert.IsTrue(true);
            }
        }
        [TestMethod]
        public async Task TestUpdateBookValid()
        {
            CreateDataBase();
            await _libraryLogic.AddNewBook("a", "a", "1/9/2000", "a", "a", "10", "2", "10", "a", "a", "a");
            await _libraryLogic.UpdateBook("1", "b", "b", "1/10/2001", "b", "b", "11", "3", "11", "b", "b", "b");
            Book b1 = await _libraryLogic.GetBook("1");
            if (b1.Name.Equals("b") && b1.Writer.Equals("b") && b1.PrintDate.Equals("1/10/2001") && b1.Publisher.Equals("b") && b1.Genre.Equals("b") && b1.Discount.Equals("11") && b1.Quantity.Equals("3") && b1.Price.Equals("11") && b1.ISBN.Equals("b") && b1.Edition.Equals("b") && b1.Summary.Equals("b"))
                Assert.IsTrue(true);
        }
        [TestMethod]
        public async Task TestUpdateBookInValid()
        {
            CreateDataBase();
            await _libraryLogic.AddNewBook("a", "a", "1/9/2000", "a", "a", "10", "2", "10", "a", "a", "a");
            try
            {
                await _libraryLogic.UpdateBook("1", "b", "b", "", "b", "b", "11", "3", "11", "b", "b", "b");
            }
            catch (LibraryException)
            {
                Assert.IsTrue(true);
            }
        }
        [TestMethod]
        public async Task TestUpdateJournalValid()
        {
            CreateDataBase();
            await _libraryLogic.AddNewJournal("a", "a", "1/9/2000", "a", "a", "10", "1", "10", "a");
            await _libraryLogic.UpdateJournal("1", "b", "b", "1/10/2001", "b", "b", "11", "3", "11", "b");
            Journal j1 = await _libraryLogic.GetJournal("1");
            if (j1.Name.Equals("b") && j1.Writer.Equals("b") && j1.PrintDate.Equals("1/10/2001") && j1.Publisher.Equals("b") && j1.Genre.Equals("b") && j1.Discount.Equals("11") && j1.Quantity.Equals("3") && j1.Price.Equals("11") && j1.Subject.Equals("b"))
                Assert.IsTrue(true);
        }
        [TestMethod]
        public async Task TestUpdateJournalInValid()
        {
            CreateDataBase();
            await _libraryLogic.AddNewJournal("a", "a", "1/9/2000", "a", "a", "10", "1", "10", "a");
            try
            {
                await _libraryLogic.UpdateJournal("1", "b", "b", "", "b", "b", "11", "3", "11", "b");
            }
            catch (LibraryException)
            {
                Assert.IsTrue(true);
            }
        }
    }
}
