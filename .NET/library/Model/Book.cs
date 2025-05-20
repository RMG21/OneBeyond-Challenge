namespace OneBeyondApi.Model
{
    public class Book
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required Author Author { get; set; }
        public BookFormat Format { get; set; }
        public required string ISBN { get; set; }
    }
}
