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

        public BookRepository()
        {
            _books = new List<Book>() 
            { 
                new Book(1, "War of the Worlds", 500),
                new Book(2, "We", 239),
                new Book(3, "Dune", 599),
                new Book(4, "Shadows of Treachery", 299)
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

                switch (sortBy)
                {

                    case "":
                        tempBooks = tempBooks.OrderBy(m => m.Title).ToList();
                        break;
                    case "titleasc":
                        tempBooks = tempBooks.OrderBy(m => m.Title).ToList();
                        break;
                    case "titledesc":
                        tempBooks = tempBooks.OrderByDescending(m => m.Title).ToList();
                        break;



                }

            }


            return _books;
        }

    }
}
