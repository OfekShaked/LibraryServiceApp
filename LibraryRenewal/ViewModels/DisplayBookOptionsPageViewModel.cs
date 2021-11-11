using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using LibraryRenewal.BLL.Exceptions;
using LibraryRenewal.Common.Enums;
using LibraryRenewal.Common.Models;
using LibraryRenewed.Design.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace LibraryRenewal.ViewModels
{
    public class DisplayBooksOptionsPageViewModel : INotifyPropertyChanged
    {
        public NavigationData NavData { get; private set; }
        INavigationService _navigationService;
        public RelayCommand SellCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand ClearCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }
        public RelayCommand SearchCommand { get; set; }
        public RelayCommand ReturnCommand { get; set; }
        public RelayCommand ReportCommand { get; set; }
        private object _selectedBook;
        public object SelectedBook { 
            get
            {
                return _selectedBook;
            }
            set
            {
                _selectedBook = value;
                BookTableSelectionChanged();
                OnPropertyChanged(String.Empty);
            }
        }
        private object _selectedJournal;
        public object SelectedJournal
        {
            get
            {
                return _selectedJournal;
            }
            set
            {
                _selectedJournal = value;
                JournalTableSelectionChanged();
                OnPropertyChanged(String.Empty);
            }
        }
        public object SelectedItem { get; set; }
        private bool _isJournalSelected;
        public bool IsJournalsSelected
        {
            get { return _isJournalSelected; }
            set
            {
                _isJournalSelected = value;
                _isBookSelected = !value;
                ListViewChanged();
                OnPropertyChanged(String.Empty);
            }
        }
        private bool _isBookSelected;
        public bool IsBooksSelected
        {
            get { return _isBookSelected; }
            set
            {
                _isBookSelected = value;
                _isJournalSelected = !value;
                ListViewChanged();
            }
        }
        public bool IsBiggerThanYearSelected { get; set; }
        public bool IsSmallerThanYearSelected { get; set; }
        public bool IsBiggerThanDiscountSelected { get; set; }
        public bool IsSmallerThanDiscountSelected { get; set; }

        private ObservableCollection<Book> _books;
        public ObservableCollection<Book> Books
        {
            get { return _books; }
            set
            {
                _books = value;
                this.OnPropertyChanged();
            }
        }
        private ObservableCollection<Journal> _journals;
        public ObservableCollection<Journal> Journals
        {
            get { return _journals; }
            set
            {
                _journals = value;
                this.OnPropertyChanged();
            }
        }
        private ObservableCollection<AbstractItem> _items;

        public ObservableCollection<AbstractItem> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                this.OnPropertyChanged();
            }
        }
        public Visibility BooksListVisibility { get; set; }
        public Visibility JournalListVisibility { get; set; }
        public Visibility ItemListVisibility { get; set; }
        public string TxtName { get; set; }
        public string TxtWriter { get; set; }
        public DateTimeOffset PrintDate { get; set; }
        public string TxtGenre { get; set; }
        public string TxtDiscount { get; set; }
        public string TxtPublisher { get; set; }
        public string TxtQuantity { get; set; }
        public string TxtPrice { get; set; }
        public string TxtIsbn { get; set; }
        public string TxtEdition { get; set; }
        public string TxtSummary{ get; set; }
        public string TxtSubject { get; set; }
        private string _publisherSearch;
        public string PublisherSearch
        {
            get { return _publisherSearch; }
            set { 
                _publisherSearch = value;
                PublisherTextChanged();
            }
        }
        private string _authorSearch;
        public string AuthorSearch
        {
            get { return _authorSearch; }
            set
            {
                _authorSearch = value;
                AuthorTextChanged();
            }
        }
        private string _genreSearch;
        public string GenreSearch
        {
            get { return _genreSearch; }
            set
            {
                _genreSearch = value;
                GenreTextChanged();
            }
        }
        public string YearSearch { get; set; }
        public string DiscountSearch { get; set; }
        private string _nameSearch;

        public event PropertyChangedEventHandler PropertyChanged;

        public string NameSearch
        {
            get { return _nameSearch; }
            set
            {
                _nameSearch = value;
                NameTextChanged();
            }
        }
        public List<string> GenreSuggestions { get; set; }
        public List<string> PublisherSuggestions { get; set; }
        public List<string> AuthorSuggestions { get; set; }
        public List<string> NameSuggestions { get; set; }
        public DisplayBooksOptionsPageViewModel(NavigationData navigationData, INavigationService service)
        {
            NavData = navigationData;
            _navigationService = service;
            IsBooksSelected = true;
            IsBiggerThanDiscountSelected = true;
            IsBiggerThanYearSelected = true;
            BooksListVisibility = Visibility.Visible;
            JournalListVisibility = Visibility.Collapsed;
            ItemListVisibility = Visibility.Collapsed;
            SellCommand = new RelayCommand(SellClicked);
            AddCommand = new RelayCommand(AddClicked);
            ClearCommand = new RelayCommand(ClearValues);
            DeleteCommand = new RelayCommand(DeleteClicked);
            UpdateCommand = new RelayCommand(UpdateClicked);
            SearchCommand = new RelayCommand(SearchClicked);
            ReportCommand = new RelayCommand(ReportClicked);
            ReturnCommand = new RelayCommand(ReturnClicked);
        }

        private async void SellClicked()
        {
            if (IsBooksSelected)
            {
                Book book = SelectedBook as Book;
                if (book != null && book.Quantity > 0)
                {
                    try
                    {
                        await NavData.LibraryLogic.SellBook(book.ItemID.ToString(), NavData.UsernameConnected);
                        MessageDialog md = new MessageDialog("Book Sold Successfully!");
                        await md.ShowAsync();
                    }
                    catch (BLLBookException)
                    {
                        MessageDialog md = new MessageDialog("Cannot sell a book atm try again later");
                        await md.ShowAsync();
                    }
                }
                else
                {
                    MessageDialog md = new MessageDialog("No item chosen, please choose an item to sell");
                    await md.ShowAsync();
                }
            }
            else if (IsJournalsSelected)
            {
                Journal journal = SelectedJournal as Journal;
                if (journal != null && journal.Quantity > 0)
                {
                    try
                    {
                        await NavData.LibraryLogic.SellJournal(journal.ItemID.ToString(), NavData.UsernameConnected);
                        MessageDialog md = new MessageDialog("Journal Sold Successfully!");
                        await md.ShowAsync();
                    }
                    catch (BLLBookException)
                    {
                        MessageDialog md = new MessageDialog("Cannot sell a journal atm try again later");
                        await md.ShowAsync();
                    }
                }
                else
                {
                    MessageDialog md = new MessageDialog("No item chosen, please choose an item to sell");
                    await md.ShowAsync();
                }
            }
            else
            {
                AbstractItem ai = SelectedItem as AbstractItem;
                if (ai != null && ai.Quantity > 0)
                {
                    await NavData.LibraryLogic.SellItem(ai.ItemID.ToString(), NavData.UsernameConnected);
                    MessageDialog md = new MessageDialog("Item Sold Successfully!");
                    await md.ShowAsync();
                }
                else
                {
                    MessageDialog md = new MessageDialog("No item chosen, please choose an item to sell");
                    await md.ShowAsync();
                }
            }
            ClearValues();
            ListViewChanged();
        }

        private void BookTableSelectionChanged()
        {
            Book selectedBook = SelectedBook as Book;
            if(selectedBook != null)
            {
                TxtName = selectedBook.Name;
                TxtWriter = selectedBook.Writer;
                PrintDate = selectedBook.PrintDate;
                TxtPublisher = selectedBook.Publisher;
                TxtGenre = selectedBook.Genre.Name;
                TxtDiscount = selectedBook.Discount.ToString();
                TxtQuantity = selectedBook.Quantity.ToString();
                TxtPrice = selectedBook.Price.ToString();
                TxtIsbn = selectedBook.ISBN;
                TxtEdition = selectedBook.Edition;
                TxtSummary = selectedBook.Summary;
            }
        }
        private void JournalTableSelectionChanged()
        {
            Journal selectedJournal = SelectedJournal as Journal;
            if (selectedJournal != null)
            {
                TxtName = selectedJournal.Name;
                TxtWriter = selectedJournal.Writer;
                PrintDate = selectedJournal.PrintDate;
                TxtPublisher = selectedJournal.Publisher;
                TxtGenre = selectedJournal.Genre.Name;
                TxtDiscount = selectedJournal.Discount.ToString();
                TxtQuantity = selectedJournal.Quantity.ToString();
                TxtPrice = selectedJournal.Price.ToString();
                TxtSubject = selectedJournal.Subject;
            }
        }
        private async void ListViewChanged()
        {
            ItemListVisibility = Visibility.Collapsed;
            if (IsBooksSelected)
            {
                BooksListVisibility = Visibility.Visible;
                JournalListVisibility = Visibility.Collapsed;
                if (NavData.UserType==UserType.Employee)
                {
                    Books = new ObservableCollection<Book>(await NavData.LibraryLogic.GetAllBooks());
                }
                else
                {
                    Books = new ObservableCollection<Book>(await NavData.LibraryLogic.GetAllAvailableBooks());
                }
            }
            else
            {
                BooksListVisibility = Visibility.Collapsed;
                JournalListVisibility = Visibility.Visible;
                if (NavData.UserType == UserType.Employee)
                {
                    Journals = new ObservableCollection<Journal>(await NavData.LibraryLogic.GetAllJournals());
                }
                else
                {
                    Journals = new ObservableCollection<Journal>(await NavData.LibraryLogic.GetAllAvailableJournals());
                }
            }
        }
        private void ClearValues()
        {
            TxtName = "";
            TxtWriter = "";
            PrintDate= DateTime.Now;
            TxtPublisher="";
            TxtGenre = "";
            TxtDiscount= "";
            TxtQuantity= "";
            TxtPrice = "";
            TxtIsbn = "";
            TxtEdition ="";
            TxtSummary = "";
            PublisherSearch = "";
            AuthorSearch="";
            GenreSearch = "";
            YearSearch = "";
            DiscountSearch = "";
            NameSearch = "";
        }
        private async void AddClicked()
        {
            if (IsBooksSelected)
            {
                if (TxtName == "" || TxtWriter == "" || PrintDate == null || TxtGenre == "" || TxtDiscount == "" || TxtQuantity == "" || TxtPrice == "" || TxtIsbn == "" || TxtEdition == "" || TxtSummary == "")
                {
                    MessageDialog medi = new MessageDialog("Cannot add a new item, Fill all the fields!");
                    await medi.ShowAsync();
                    return;
                }
                string name = TxtName;
                string writer = TxtWriter;
                string printDate = PrintDate.Date.ToString();
                string publisher = TxtPublisher;
                string genre = TxtGenre;
                string discount = TxtDiscount;
                string quantity = TxtQuantity;
                string price = TxtPrice;
                string isbn = TxtIsbn;
                string edition = TxtEdition;
                string summary = TxtSummary;
                try
                {
                    await NavData.LibraryLogic.AddNewBook(name, writer, printDate, publisher, genre, discount, quantity, price, isbn, edition, summary);
                    MessageDialog md = new MessageDialog("Book Added Successfully!");
                    await md.ShowAsync();
                }
                catch (Exception ex)
                {
                    MessageDialog md = new MessageDialog("Cannot add a book atm try again later");
                    await md.ShowAsync();
                }
            }
            else if (IsJournalsSelected == true)
            {
                if (TxtName == "" || TxtWriter == "" || PrintDate == null || TxtGenre == "" || TxtDiscount == "" || TxtQuantity == "" || TxtPrice == "" || TxtSubject == "")
                {
                    MessageDialog medi = new MessageDialog("Cannot add a new item, Fill all the fields!");
                    await medi.ShowAsync();
                    return;
                }
                string name = TxtName;
                string writer = TxtWriter;
                string printDate = PrintDate.Date.ToString();
                string publisher = TxtPublisher;
                string genre = TxtGenre;
                string discount = TxtDiscount;
                string quantity = TxtQuantity;
                string price = TxtPrice;
                string subject = TxtSubject;
                try
                {
                    await NavData.LibraryLogic.AddNewJournal(name, writer, printDate, publisher, genre, discount, quantity, price, subject);
                    MessageDialog md = new MessageDialog("Journal Added Successfully!");
                    await md.ShowAsync();
                }
                catch (BLLJournalException)
                {
                    MessageDialog md = new MessageDialog("Cannot add a journal atm try again later");
                    await md.ShowAsync();
                }
            }
            else
            {
                MessageDialog md = new MessageDialog("Cannot add a general item, chooose book or journal!");
                await md.ShowAsync();
                return;
            }
            ListViewChanged();
            ClearValues();
        }
        private async void DeleteClicked()
        {
            if (IsBooksSelected)
            {
                Book b1 = SelectedBook as Book;
                try
                {
                    if (b1 != null)
                    {
                        await NavData.LibraryLogic.DeleteBook(b1.ItemID.ToString());
                        MessageDialog md = new MessageDialog("Book Deleted Successfully!");
                        await md.ShowAsync();
                    }
                    else
                    {
                        MessageDialog md = new MessageDialog("Please choose a book to delete!");
                        await md.ShowAsync();
                    }
                }
                catch (BLLBookException)
                {
                    MessageDialog md = new MessageDialog("Cannot delete a book atm try again later");
                    await md.ShowAsync();
                }

            }
            else if (IsJournalsSelected)
            {
                Journal j1 = SelectedJournal as Journal;
                try
                {
                    if (j1 != null)
                    {
                        await NavData.LibraryLogic.DeleteJournal(j1.ItemID.ToString());
                        MessageDialog md = new MessageDialog("Journal Deleted Successfully!");
                        await md.ShowAsync();
                    }
                    else
                    {
                        MessageDialog md = new MessageDialog("Please choose a book to delete!");
                        await md.ShowAsync();
                    }
                }
                catch (BLLJournalException)
                {
                    MessageDialog md = new MessageDialog("Cannot delete a journal atm try again later");
                    await md.ShowAsync();
                }
            }
            ListViewChanged();
            ClearValues();
        }
        private async void UpdateClicked()
        {
            if (IsBooksSelected)
            {
                if (TxtName == "" || TxtWriter == "" || PrintDate == null || TxtGenre == "" || TxtDiscount == "" || TxtQuantity == "" || TxtPrice == "" || TxtIsbn == "" || TxtEdition == "" || TxtSummary == "")
                {
                    MessageDialog med = new MessageDialog("Fill All fields in order to update book!");
                    await med.ShowAsync();
                    return;
                }
                Book b1 = SelectedBook as Book;
                try
                {
                    if (b1 != null)
                    {
                        await NavData.LibraryLogic.UpdateBook(b1.ItemID.ToString(), TxtName, TxtWriter, PrintDate.Date.ToString(), TxtPublisher, TxtGenre, TxtDiscount, TxtQuantity, TxtPrice, TxtIsbn, TxtEdition, TxtSummary);
                        MessageDialog md = new MessageDialog("Book Updated Successfully!");
                        await md.ShowAsync();
                    }
                }
                catch (BLLBookException)
                {
                    MessageDialog md = new MessageDialog("Cannot update a book atm try again later");
                    await md.ShowAsync();
                }

            }
            else if (IsJournalsSelected)
            {
                if (TxtName == "" || TxtWriter == "" || PrintDate == null || TxtGenre == "" || TxtDiscount == "" || TxtQuantity == "" || TxtPrice == "" || TxtSubject == "")
                {
                    MessageDialog med = new MessageDialog("Fill All fields in order to update journal!");
                    await med.ShowAsync();
                    return;
                }
                Journal j1 = SelectedJournal as Journal;
                try
                {
                    if (j1 != null)
                    {
                        await NavData.LibraryLogic.UpdateJournal(j1.ItemID.ToString(), TxtName, TxtWriter, PrintDate.Date.ToString(), TxtPublisher, TxtGenre, TxtDiscount, TxtQuantity, TxtPrice, TxtSubject);
                        MessageDialog md = new MessageDialog("Journal Updated Successfully!");
                        await md.ShowAsync();
                    }
                }
                catch (BLLJournalException)
                {
                    MessageDialog md = new MessageDialog("Cannot update a journal atm try again later!");
                    await md.ShowAsync();
                }
            }
            ListViewChanged();
            ClearValues();
        }
        private async Task<List<AbstractItem>> SearchItems()
        {
            BiggerSmaller discountBorS = BiggerSmaller.Smaller;
            BiggerSmaller yearBorS = BiggerSmaller.Smaller;
            if (IsBiggerThanDiscountSelected)
            {
                discountBorS = BiggerSmaller.Bigger;
            }
            else if (IsSmallerThanDiscountSelected)
            {
                discountBorS = BiggerSmaller.Smaller;
            }
            if (IsBiggerThanYearSelected)
            {
                yearBorS = BiggerSmaller.Bigger;
            }
            else if (IsBiggerThanDiscountSelected)
            {
                yearBorS = BiggerSmaller.Smaller;
            }
            try
            {
                List<AbstractItem> items = await NavData.LibraryLogic.SearchItems(YearSearch, yearBorS, PublisherSearch, AuthorSearch, GenreSearch, DiscountSearch, discountBorS,NameSearch);
                return items;
            }
            catch (BLLGeneralException)
            {
                MessageDialog md = new MessageDialog("Cannot search items atm try again later");
                await md.ShowAsync();
                return null;
            }
        }
        private async void SearchClicked()
        {
            try
            {
                _isBookSelected = false;
                _isJournalSelected = false;
                BooksListVisibility = Visibility.Collapsed;
                JournalListVisibility = Visibility.Collapsed;
                ItemListVisibility = Visibility.Visible;
                Items = new ObservableCollection<AbstractItem>(await SearchItems());
            }
            catch (Exception)
            {
                MessageDialog md = new MessageDialog("Cannot search items atm try again later");
                await md.ShowAsync();
            }
        }
        private async void PublisherTextChanged()
        {
            if (PublisherSearch.Length >= 1)
            {
                try
                {
                    PublisherSuggestions = await NavData.LibraryLogic.GetPublisherSuggestions(_publisherSearch);
                    OnPropertyChanged("PublisherSuggestions");
                }
                catch { }
            }
            else
            {
                PublisherSuggestions= new List<string>() { "No Suggestions..." };
                OnPropertyChanged("PublisherSuggestions");
            }
        }
        private async void GenreTextChanged()
        {
            if (GenreSearch.Length >= 1)
            {
                try
                {
                    GenreSuggestions = await NavData.LibraryLogic.GetGenreSuggestions(_genreSearch);
                    OnPropertyChanged("GenreSuggestions");
                }
                catch { }
            }
            else
            {
                GenreSuggestions = new List<string>() { "No Suggestions..." };
                OnPropertyChanged("GenreSuggestions");
            }
        }
        private async void AuthorTextChanged()
        {
            if (AuthorSearch.Length >= 1)
            {
                try
                {
                    AuthorSuggestions = await NavData.LibraryLogic.GetAuthorSuggestions(_authorSearch);
                    OnPropertyChanged("AuthorSuggestions");
                }
                catch { }
            }
            else
            {
                AuthorSuggestions = new List<string>() { "No Suggestions..." };
                OnPropertyChanged("AuthorSuggestions");
            }
        }
        private async void NameTextChanged()
        {
            if (NameSearch.Length >= 1)
            {
                try
                {
                    NameSuggestions = await NavData.LibraryLogic.GetNameSuggestions(_nameSearch);
                    OnPropertyChanged("NameSuggestions");
                }
                catch { }
            }
            else
            {
                NameSuggestions = new List<string>() { "No Suggestions..." };
                OnPropertyChanged("NameSuggestions");
            }
        }
        private void ReturnClicked()
        {
            if (NavData.UserType == UserType.Manager)
            {
                _navigationService.NavigateTo("ManagerMainPage");
            }
            if (NavData.UserType == UserType.Employee)
            {
                _navigationService.NavigateTo("MainPage");
            }
        }
        private async void ReportClicked()
        {
            try
            {
                NavData.searchResult = await SearchItems();
                _navigationService.NavigateTo("ReportPage");
            }
            catch
            {
                MessageDialog md = new MessageDialog("Cannot create a report atm!");
                await md.ShowAsync();
            }
        }
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
