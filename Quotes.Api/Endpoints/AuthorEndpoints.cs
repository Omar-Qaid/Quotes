using Quotes.Core.Entities;
using Quotes.Core.Interfaces;

namespace Quotes.Api.Endpoints
{
    public static class AuthorEndpoints  //Minimal APIs
    {
        public static void MapAuthorEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/MinimalAPIs/Author");

            group.MapGet("/", async (IAuthorService _author) =>
            {
                return await _author.GetAuthorListAsync();

            }).WithName("listAuthors");

            group.MapGet("/{id}", async (int id, IAuthorService _author) =>
            {
                if (await _author.FindAuthorAsync(id) is Author model)
                    return TypedResults.Ok(model);
                return Results.NotFound();

            }).WithName("getAuthorById");

            group.MapPut("/{id}", async (int id, Author model, IAuthorService _author) =>
            {
                if (await _author.FindAuthorAsync(id) is Author author)
                {
                    _author.UpdateAuthor(model);
                    await _author.SaveChangeAsync();
                    return TypedResults.Ok(model);
                }
                return Results.NoContent();
            }).WithName("updateAuthor");

            group.MapPost("/", async (Author model, IAuthorService _author) =>
            {
                await _author.AddAuthorAsync(model);
                await _author.SaveChangeAsync();
                return TypedResults.Created($"/Authors/{model.Id}", model);
            }).WithName("addAuthor");

            group.MapDelete("/{id}", async (int id, IAuthorService _author) =>
            {
                if (await _author.FindAuthorAsync(id) is Author model)
                {
                    _author.DeleteAuthor(model);
                    _author.SaveChange();
                    return TypedResults.Ok(model);
                }
                return Results.NotFound();
            }).WithName("deleteAuthor");
        }
    }
}
