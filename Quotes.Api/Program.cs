using Microsoft.EntityFrameworkCore;
using Quotes.Core.Interfaces;
using Quotes.EF.Repositories;
using Quotes.EF;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Quotes.Api.Endpoints;
using System;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSingleton<HtmlEncoder>(HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.BasicLatin, UnicodeRanges.Arabic }));
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IQuoteService, QuoteService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapAuthorEndpoints();
app.MapQuoteEndpoints();


app.Run();

