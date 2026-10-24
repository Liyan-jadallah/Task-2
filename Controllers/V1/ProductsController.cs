using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace VersioningDemo.Controllers.V1;

[ApiController]
[ApiVersion(1)]
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
            price = 25000
        };

        return Ok(product);
    }
}