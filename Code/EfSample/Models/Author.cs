using System.Collections.Generic;

namespace EfSample.Models
{
    public class Author
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<Book> Books { get; } = new List<Book>();
    }
}