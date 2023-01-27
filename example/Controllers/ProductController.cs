using example.DTO;
using example.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace example.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductsdbContext _context;

        public ProductController(ProductsdbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await _context.Products.ToListAsync());
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetProduct([FromRoute] int id)
        {
            var product = await _context.Products.FindAsync(id);
            if(product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }


        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductDTO addProductRequest)
        {
            var product = new Product()
            {
                Name = addProductRequest.Name,
                Description = addProductRequest.Description,
                Price = addProductRequest.Price
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return Ok(product);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, ProductDTO updateProductRequest)
        {
            var product = await _context.Products.FindAsync(id);
            if(product != null)
            {
                product.Name= updateProductRequest.Name;
                product.Description= updateProductRequest.Description;
                product.Price= updateProductRequest.Price;

                await _context.SaveChangesAsync();
                return Ok(product);
            }

            return NotFound();

        }


        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            var product = await _context.Products.FindAsync(id);
            if(product != null )
            {
                _context.Remove(product);
                await _context.SaveChangesAsync();
                return Ok(product);
            }

            return NotFound();
        }
        
        

    }
}
