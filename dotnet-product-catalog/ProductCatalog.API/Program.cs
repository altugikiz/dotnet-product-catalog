using Microsoft.EntityFrameworkCore;
using ProductCatalog.API.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. --- Service Records (DIP Container) ---

// Controller Structer
builder.Services.AddControllers();

// Swagger / OpenAI Documents
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// To Add Context of Database
// Temporary Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("ProductDb"));

// -----------------------------------------------------------

var app = builder.Build();

// 2. --- Middleware Pipeline ---

// To open Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Redirect incoming requests to Controller classes
app.MapControllers();

app.Run();