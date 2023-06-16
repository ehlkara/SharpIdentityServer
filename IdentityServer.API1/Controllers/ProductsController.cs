using IdentityServer.API1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.API1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // /api/products
        [Authorize(Policy = "ReadProduct")]
        [HttpGet]
        public IActionResult GetProducts()
        {
            var productList = new List<Products>() { new Products() { Id=1, Name="Pen",Price=100, Stock=500},
            new Products() { Id=2, Name="Rubber",Price=100, Stock=500},
            new Products() { Id=3, Name="Notebook",Price=100, Stock=500},
            new Products() { Id=4, Name="Book",Price=100, Stock=500},
            new Products() { Id=5, Name="Bant",Price=100, Stock=500}};
            return Ok(productList);
        }

        [Authorize(Policy ="UpdateOrCreate")]
        [HttpPut("UpdateProduct")]
        public IActionResult UpdateProduct(int id)
        {
            return Ok($"The product with an id of {id} has been updated.");
        }
        [Authorize(Policy = "UpdateOrCreate")]
        [HttpPost("CreateProduct")]
        public IActionResult CreateProduct(Products products)
        {
            return Ok(products);
        }
    }
}
