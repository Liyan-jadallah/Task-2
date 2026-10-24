using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace VersioningDemo.Controllers.V2;

[ApiController]
[ApiVersion(2)]
[Route("api/v{version:apiVersion}/products")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetProducts()
    {
        var product = new
        {
            id = 1,
            name = "Laptop",
            price = 25000,
            categoryName = "Electronics"
        };

        return Ok(product);
    }
}