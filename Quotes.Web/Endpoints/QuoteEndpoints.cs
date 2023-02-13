using Quotes.Core.Interfaces;

namespace Quotes.Web.Endpoints
{

    public static class QuoteEndpoints //MinimalAPIs 
    {
        public static void MapQuoteEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/");
            group.MapGet("/getRandomQuoteApiAsync/", async (IQuoteService _Quote) =>
            {
                return await _Quote.GetRandomQuoteAsync(null);

            }).WithName("getRandomQuoteApiAsync");
      
        }
     
    }
  
}
