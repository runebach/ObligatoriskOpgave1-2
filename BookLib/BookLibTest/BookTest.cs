using BookLib;
using System.Diagnostics;

namespace BookLibTest
{
    [TestClass]
    public class BookTest
    {
        public Book TestBook;
        public Book BookShortTitle = new Book("lo", 500) { Id = 1};
        public Book BookNullTitle = new Book(null, 500) { Id = 1 };
        public Book BookGood = new Book("test", 500) { Id = 1 };


        [TestMethod]
        public void ValidateShortTitleTest()
        {
            Assert.ThrowsException<ArgumentException>(() => BookShortTitle.ValidateAll());
        }

        [TestMethod]
        public void ValidateNullTitleTest()
        {
            Assert.ThrowsException<ArgumentNullException>(() => BookNullTitle.ValidateAll());
        }

        [TestMethod]
        [DataRow("hej")]
        [DataRow("supersejtesttitel")]
        public void ValidateGoodTitles(string title)
        {
            TestBook = new Book(title, 500) { Id = 1 };
            TestBook.ValidateAll();
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(1201)]
        public void ValidateWrongPriceTest(double price)
        {
            TestBook = new Book("test", price) { Id = 1 };
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => TestBook.ValidateAll());

        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(500)]
        [DataRow(1200)]
        public void ValidateGoodPriceTest(double price)
        {
            TestBook = new Book("test", price) { Id = 1 };
            TestBook.ValidateAll();
        }

        [TestMethod]
        public void ToStringTest()
        {
            Assert.AreEqual("1 test 500", BookGood.ToString());
        }


        [TestMethod]
        public void HashAndEqualTest()
        {
            Book copyBook = new Book("test", 500) { Id = 1 };
            Assert.AreEqual(BookGood.Equals(copyBook), true);

        }


    }
}