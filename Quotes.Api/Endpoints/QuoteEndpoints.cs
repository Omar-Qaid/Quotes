using Microsoft.AspNetCore.Http.HttpResults;
using Quotes.Core.Entities;
using Quotes.Core.Interfaces;

namespace Quotes.Api.Endpoints
{
    public static class QuoteEndpoints //MinimalAPIs
    {
        public static void MapQuoteEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/MinimalAPIs/Quote");

            group.MapGet("/listQuotes", async (IQuoteService _Quote) =>
            {
                return await _Quote.GetQuoteListAsync();

            }).WithName("listQuotes");

            group.MapGet("/getRandomQuote/", async (IQuoteService _Quote) =>
            {
                return await _Quote.GetRandomQuoteAsync(null);

            }).WithName("getRandomQuote");

            group.MapGet("/getQuoteByAuthor/{authorId}", async (int authorId, IQuoteService _Quote) =>
            {
                return await _Quote.GetQuoteByAuthorAsync(authorId);

            }).WithName("getQuoteByAuthor");
       
            group.MapGet("/FindQuote/{id}", async (int id, IQuoteService _Quote) =>
            {
                return await _Quote.FindQuoteAsync(id);

            }).WithName("findQuote");

            group.MapPut("/updateQuote/{id}", async (int id, Quote model, IQuoteService _Quote) =>
            {
                if (await _Quote.FindQuoteAsync(id) is Quote quote)
                {
                    _Quote.UpdateQuote(model);
                    await _Quote.SaveChangeAsync();
                    return TypedResults.Ok(model);
                }
                return Results.NoContent();

            }).WithName("updateQuote");

            group.MapPost("/addQuote", async (Quote model, IQuoteService _Quote) =>
            {
                await _Quote.AddQuoteAsync(model);
                await _Quote.SaveChangeAsync();
                return TypedResults.Created($"/Authors/{model.Id}", model);
           
            }).WithName("addQuote");

            group.MapDelete("/deleteQuote/{id}", async (int id, IQuoteService _Quote) =>
            {
                if (await _Quote.FindQuoteAsync(id) is Quote model)
                {
                    _Quote.DeleteQuote(model);
                    _Quote.SaveChange();
                    return TypedResults.Ok(model);
                }
                return Results.NotFound();

            }).WithName("deleteQuote");
        }
    }
}
