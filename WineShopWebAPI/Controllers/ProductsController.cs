using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WineShopWebAPI.Models;
using Microsoft.Identity.Web.Resource;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WineShopWebAPI.Authentication;

namespace WineShopWebAPI.Controllers
{
    [Authorize(Roles = "PurchaseManager")]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public ProductsController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _context = context;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Product_ID)
            {
                return BadRequest();
            }

            // Check if the product with the given ID exists in the database
            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null)
            {
                // If the product does not exist, return a 404 Not Found response
                return NotFound();
            }

            var existingSupplier = await _context.Suppliers.FindAsync(product.Supplier_ID);
            if (existingSupplier == null)
            {
                // If the supplier does not exist, return a 404 Not Found response
                return NotFound($"Supplier with ID {product.Supplier_ID} not found");
            }


            // Assign the existing supplier to the product
            existingProduct.Supplier = existingSupplier;
            // Update the properties of the existing product with the provided values
            existingProduct.Product_Name = product.Product_Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.Supplier_ID = product.Supplier_ID; // You may choose not to update the Supplier_ID if it shouldn't be modified


            try
            {


                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Product
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            // Check if the provided Supplier_ID exists
            var existingSupplier = await _context.Suppliers.FindAsync(product.Supplier_ID);
            if (existingSupplier == null)
            {
                // If the supplier does not exist, return a 404 Not Found response
                return NotFound($"Supplier with ID {product.Supplier_ID} not found");
            }

            // Assign the existing supplier to the product
            product.Supplier = existingSupplier;

            // Add the product to the context
            _context.Products.Add(product);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return a 201 Created response with the created product
            return CreatedAtAction("GetProduct", new { id = product.Product_ID }, product);
        }


        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Product_ID == id);
        }



    }
}


