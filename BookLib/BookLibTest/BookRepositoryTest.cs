using BookLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibTest
{
    [TestClass]
    public class BookRepositoryTest 
    {

        private BookRepository _bookRepo;
        private List<Book> tempBooks;


        [TestInitialize]
        public void Initialize() 
        {
            _bookRepo = new BookRepository();
            tempBooks = new List<Book>();
        }

        [TestMethod]
        public void GetAllBooksTest() //Test af GetAllBooks metode
        {
            tempBooks = _bookRepo.GetAllBooks();
            Assert.AreEqual("War of the Worlds", tempBooks.First().Title); // Tjekker om default metoden giver bøger i den rigtige rækkefølge
            Assert.AreEqual(4, tempBooks.Count()); // Tjekker om default metoden giver det rigtige antal bøger

            tempBooks = _bookRepo.GetAllBooks(title: "We");
            Assert.AreEqual(1, tempBooks.Count()); // Tjekker om der kun bliver givet én bog (We)

            tempBooks = _bookRepo.GetAllBooks(lowerPrice: 250);
            Assert.AreEqual(3, tempBooks.Count()); // Tjekker om der er præcist 3 bøger i listen der har en pris over 250

            tempBooks = _bookRepo.GetAllBooks(upperPrice: 250);
            Assert.AreEqual(1, tempBooks.Count()); // Tjekker om der er præcist 1 bog i listen der har en pris under 250

            tempBooks = _bookRepo.GetAllBooks(sortBy: "tiTleAsc");
            Assert.AreEqual("Dune", tempBooks.First().Title); // TJekker om Dune er først i listen når vi sorterer efter titel. Tjekker også om ToLower virker i guess

            tempBooks = _bookRepo.GetAllBooks(sortBy: "titledesc");
            Assert.AreEqual("We", tempBooks.First().Title); // Tjekker om We er først i listen når vi sorterer efter titel descending

            tempBooks = _bookRepo.GetAllBooks(sortBy: "priceasc");
            Assert.AreEqual(239, tempBooks.First().Price); // Tjekker om første bog i listen har den laveste pris

            tempBooks = _bookRepo.GetAllBooks(sortBy: "pricedesc");
            Assert.AreEqual(599, tempBooks.First().Price); //Tjekker om første bog i listen har den højeste pris


        }

        [TestMethod]
        public void AddBookTest()
        {
            tempBooks = _bookRepo.GetAllBooks();

            Book bookGood = new Book() {Title = "Pathfinder Second Edition: Core Rulebook", Price = 699 };
            _bookRepo.AddBook(bookGood);
            Assert.AreEqual("Pathfinder Second Edition: Core Rulebook", tempBooks.Last().Title); // Tjekker om sidste bogs titel matcher
            Assert.AreEqual(5, tempBooks.Count()); // Tjekker om sidste bogs pris matcher

            Book bookNullTitle = new Book() {Title = null, Price = 500 };
            Assert.ThrowsException<ArgumentNullException>(() => _bookRepo.AddBook(bookNullTitle)); // Tjekker om den rigtige exception bliver kastet med null title

            Book bookBadTitle = new Book() {Title = "", Price = 500 };
            Assert.ThrowsException<ArgumentException>(() => _bookRepo.AddBook(bookBadTitle)); // Tjekker om den rigtige exception bliver kastet med en "" title
        }

        [TestMethod]
        public void GetBookByIdTest()
        {
            Book book;
            book = _bookRepo.GetBookById(1);
            Assert.AreEqual("War of the Worlds", book.Title); // Tjekker om den hentede bog har rigtig titel

            book = _bookRepo.GetBookById(576); // Tjekker om et invalid ID returnerer et null objekt.
            Assert.AreEqual(null, book);
        }

        [TestMethod]
        public void DeleteBookTest()
        {
            tempBooks = _bookRepo.GetAllBooks();
            Assert.AreEqual("War of the Worlds", _bookRepo.DeleteBook(1).Title); // Tjekker om metoden returnerer rigtigt objekt

            Assert.AreEqual(null, _bookRepo.DeleteBook(12000)); // Tjekker om metoden returnerer null objekt med ID der ikke eksisterer
            

            Assert.AreEqual(3, tempBooks.Count()); // Tjekker om en bog er blevet fjernet
            Assert.AreEqual("We", tempBooks.First().Title); // Tjekker om den første bog har ændret sig til den rigtige
        }

        [TestMethod]
        public void UpdateBookTest()
        {
            tempBooks = _bookRepo.GetAllBooks();
            Book newBook = new Book() { Title = "Advanced Dungeons & Dragons Second Edition: Dungeon Master's Guide", Price = 349 };
            _bookRepo.UpdateBook(newBook, 1);

            Assert.AreEqual(4, tempBooks.Count()); // Tjekker om der stadig er 4 bøger i listen
            Assert.AreEqual("Advanced Dungeons & Dragons Second Edition: Dungeon Master's Guide", tempBooks.First().Title); // Tjekker om den første bog i listen har skiftet navn


            Book nullBook = _bookRepo.UpdateBook(newBook, 1259);
            Assert.AreEqual(null, nullBook); // Tjekker om et invalid ID giver et null objekt


            Book bookNullTitle = new Book() { Id = 10, Price = 100, Title = null };
            Assert.ThrowsException<ArgumentNullException>(() => _bookRepo.UpdateBook(bookNullTitle, 1)); // Tjekker om en bog med null title kaster rigtig exception

            Book bookEmptyTitle = new Book() { Id = 10, Price = 100, Title = "" };
            Assert.ThrowsException<ArgumentException>(() => _bookRepo.UpdateBook(bookEmptyTitle, 1));  // Tjekker om en bog med "" title kaster rigtig exception


        }

    }
}
