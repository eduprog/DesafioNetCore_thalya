using Desafio.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController
{
    [HttpGet()]
    public async Task<IEnumerable<Product>> GetAll()
    {
        return null;
    }
}
