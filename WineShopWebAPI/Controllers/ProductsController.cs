using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WineShopWebAPI.Models;
using Microsoft.Identity.Web.Resource;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WineShopWebAPI.Authentication;

namespace WineShopWebAPI.Controllers
{

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
        public async Task<ActionResult<IEnumerable<ProductWithSupplier>>> GetProducts()
        {
            var productsWithSuppliers = await _context.Products
                .Join(_context.Suppliers,
                    product => product.Supplier_ID,
                    supplier => supplier.Supplier_ID,
                    (product, supplier) => new ProductWithSupplier
                    {
                        Product_ID = product.Product_ID,
                        Product_Name = product.Product_Name,
                        Description = product.Description,
                        Price = product.Price,
                        Supplier_Name = supplier.Supplier_Name,
                        Supplier_ID = supplier.Supplier_ID
                    })
                .ToListAsync();

            return productsWithSuppliers;
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
        public async Task<IActionResult> PutProduct(int id, ProductRequest productRequest)
        {
            try
            {
               Product product = new Product();

                product.Product_ID = productRequest.Product_ID;
                product.Product_Name=productRequest.Product_Name;
                product.Description= productRequest.Description;
                product.Price = productRequest.Price;
                product.Supplier_ID = productRequest.Supplier_ID;

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

                await _context.SaveChangesAsync();

                return NoContent();
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
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }


        // POST: api/Product
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(ProductRequest product)
        {
            try
            {
                // Check if the provided Supplier_ID exists
                var existingSupplier = await _context.Suppliers.FindAsync(product.Supplier_ID);
                if (existingSupplier == null)
                {
                    // If the supplier does not exist, return a 404 Not Found response
                    return NotFound($"Supplier with ID {product.Supplier_ID} not found");
                }

                var newProduct = new Product()
                {
                    Price = product.Price,
                    Product_Name = product.Product_Name,
                    Description = product.Description,
                    Supplier_ID = product.Supplier_ID,
                    Supplier = existingSupplier    // Assign the existing supplier to the product
                };



                //newProduct.Supplier = existingSupplier;

                // Add the product to the context
                _context.Products.Add(newProduct);

                // Save changes to the database
                await _context.SaveChangesAsync();

                // Return a 201 Created response with the created product
                return CreatedAtAction("GetProduct", new { id = product.Product_ID }, product);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
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


        // GET: api/Products/AutoComplete
        [HttpGet("AutoComplete")]
        public IActionResult AutoComplete(string term)
        {
            try
            {
                // Query the database for products that match the term
                var products = _context.Products
                    .Where(p => p.Product_Name.Contains(term))
                    .Select(p => new { label = p.Product_Name, value = p.Product_ID })
                    .ToList();

                // Return the JSON result
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }


    }
}


