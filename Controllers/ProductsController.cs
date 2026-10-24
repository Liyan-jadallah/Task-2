using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task2EfCore.Data;
using Task2EfCore.Dtos;

namespace Task2EfCore.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts(
        [FromQuery] int pageNumber = 1, 
        [FromQuery] int pageSize = 10)
    {
        var products = await _context.Products
            .AsNoTracking()
            .OrderBy(p => p.Id)
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                CategoryName = p.Category.Name,
                RowVersion = p.RowVersion
            })
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return Ok(products);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductDto dto)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product is null)
            return NotFound("المنتج غير موجود.");

        product.Name = dto.Name;
        product.Price = dto.Price;
        product.CategoryId = dto.CategoryId;

        _context.Entry(product).Property(p => p.RowVersion).OriginalValue = dto.RowVersion;

        try
        {
            await _context.SaveChangesAsync();
            return NoContent();
        }
        catch (DbUpdateConcurrencyException)
        {
            return Conflict(new 
            { 
                message = "حدث تعارض! تم تعديل هذا المنتج بواسطة مستخدم آخر." 
            });
        }
    }
}