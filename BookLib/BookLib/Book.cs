namespace BookLib
{
    public class Book
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }

        public Book(string title, double price)
        {
            Title = title;
            Price = price;
        }

        public Book()
        {

        }

        public override string ToString()
        {
            return $"{Id} {Title} {Price}";
        }

        public void ValidateTitle()
        {
            if(Title == null)
            {
                throw new ArgumentNullException("Title must not be null");
            }
            if(Title.Length < 3)
            {
                throw new ArgumentException("Title must have a length of 3 or more characters");
            }
        }

        public void ValidatePrice()
        {
            if(Price <= 0 || Price > 1200)
            {
                throw new ArgumentOutOfRangeException("Price must be higher than 0 and no more than 1200");
            }
        }

        public void ValidateAll()
        {
            ValidateTitle();
            ValidatePrice();
        }

    }
}