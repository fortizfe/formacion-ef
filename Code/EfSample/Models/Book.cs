namespace EfSample.Models
{
    public class Book
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Isbn { get; set; }

        public string Bookcase { get; set; }

        public virtual Author Author { get; set; }
    }
}