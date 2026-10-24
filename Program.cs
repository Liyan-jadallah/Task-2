using Microsoft.EntityFrameworkCore;
using Task2EfCore.Data;
using Task2EfCore.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("Task2ProductsDb"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    
    context.Categories.Add(new Category { Id = 1, Name = "Electronics" });

    context.Products.AddRange(
        new Product { Id = 1, Name = "Laptop", Price = 25000, CategoryId = 1, IsDeleted = false, RowVersion = [1, 0, 0, 0] },
        new Product { Id = 2, Name = "Mouse", Price = 500, CategoryId = 1, IsDeleted = false, RowVersion = [1, 0, 0, 0] },
        new Product { Id = 3, Name = "Deleted Keyboard", Price = 300, CategoryId = 1, IsDeleted = true, RowVersion = [1, 0, 0, 0] }
    );

    context.SaveChanges();
}

app.MapControllers();

app.Run();