namespace Quotes.Api.DTOs
{
    public class QuoteDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int AuthorId { get; set; }

    }
}
