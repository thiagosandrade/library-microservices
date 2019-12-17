namespace Library.Books.Domain.Models
{
    public class Book : Entity
    {
        public string Name { get; set; }
        public int NumberOfPages { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
