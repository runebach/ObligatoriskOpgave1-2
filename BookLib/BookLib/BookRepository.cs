using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BookLib
{
    public class BookRepository
    {

        private List<Book> _books;
        private int _nextId = 5;

        public BookRepository()
        {
            _books = new List<Book>() 
            { 
                new Book("War of the Worlds", 500) {Id = 1},
                new Book("We", 239) {Id = 2},
                new Book("Dune", 599) {Id = 3},
                new Book("Shadows of Treachery", 299) {Id = 4}
            };

        }

        public List<Book> GetAllBooks(string? title = null, double? lowerPrice = null, double? upperPrice = null, string? sortBy = null)
        {
            List<Book> tempBooks = _books;

            if(title != null)
            {
                tempBooks = tempBooks.Where(m => m.Title.Contains(title)).ToList();
            }

            if(lowerPrice != null)
            {
                tempBooks = tempBooks.Where(m => m.Price > lowerPrice).ToList();
            }

            if(upperPrice != null)
            {
                tempBooks = tempBooks.Where(m => m.Price < upperPrice).ToList();
            }

            if(sortBy != null)
            {

                sortBy = sortBy.ToLower();

                switch (sortBy)
                {

                    case "titleasc":
                        tempBooks = tempBooks.OrderBy(m => m.Title).ToList();
                        break;
                    case "titledesc":
                        tempBooks = tempBooks.OrderByDescending(m => m.Title).ToList();
                        break;
                    case "priceasc":
                        tempBooks = tempBooks.OrderBy(m => m.Price).ToList();
                        break;
                    case "pricedesc":
                        tempBooks = tempBooks.OrderByDescending(m => m.Price).ToList();
                        break;

                }

            }


            return tempBooks ;
        }


        public Book? GetBookById(int id)
        {
            foreach(Book book in _books)
            {
                if(book.Id == id)
                {
                    return book;
                }
            }
            return null;
        }

        public Book UpdateBook(Book newBook, int id)
        {
            Book? oldBook = GetBookById(id);
            if(oldBook == null)
            {
                return null;
            }
            newBook.ValidateAll();
            oldBook.Title = newBook.Title;
            oldBook.Price = newBook.Price;
            return oldBook;

        }

        public Book AddBook(Book book)
        {
            book.ValidateAll();
            book.Id = _nextId++;
            _books.Add(book);
            return book;
        }

        public Book DeleteBook(int id)
        {
            Book? tempBook = GetBookById(id);
            if(tempBook == null)
            {
                return null;
            }
            _books.Remove(tempBook);
            return tempBook;
        }

    }
}
