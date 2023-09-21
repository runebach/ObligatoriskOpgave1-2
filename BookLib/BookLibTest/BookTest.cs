using BookLib;
using System.Diagnostics;

namespace BookLibTest
{
    [TestClass]
    public class BookTest
    {
        public Book TestBook;
        public Book BookShortTitle = new Book(1, "lo", 500);
        public Book BookNullTitle = new Book(1, null, 500);
        public Book BookGood = new Book(1, "test", 500);


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
            TestBook = new Book(1, title, 500);
            TestBook.ValidateAll();
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(1201)]
        public void ValidateWrongPriceTest(double price)
        {
            TestBook = new Book(1, "test", price);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => TestBook.ValidateAll());

        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(500)]
        [DataRow(1200)]
        public void ValidateGoodPriceTest(double price)
        {
            TestBook = new Book(1, "test", price);
            TestBook.ValidateAll();
        }

        [TestMethod]
        public void ToStringTest()
        {
            Assert.AreEqual("1 test 500", BookGood.ToString());
        }
    }
}