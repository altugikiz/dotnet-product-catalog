using Microsoft.EntityFrameworkCore;
using ProductCatalog.API.Data;
using ProductCatalog.API.Services;
using FluentValidation;
using ProductCatalog.API.Mappings;

var builder = WebApplication.CreateBuilder(args);

// 1. --- Service Records (DIP Container) ---

// Controller Structer
builder.Services.AddControllers();

builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// If someone asks you for IProductService, give them ProductService. (Scoped)
builder.Services.AddScoped<IProductService, ProductService>();

// Swagger / OpenAI Documents
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// To Add Context of Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// -----------------------------------------------------------

var app = builder.Build();

// The application creates a temporary database and prints the Seed Data when it starts up.
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated(); // Trigger
}

// 2. --- Middleware Pipeline ---

// To open Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Middleware Pipeline
app.UseMiddleware<ProductCatalog.API.Middlewares.GlobalExceptionMiddleware>();

// Redirect incoming requests to Controller classes
app.MapControllers();

app.Run();