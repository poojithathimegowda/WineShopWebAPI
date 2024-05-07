using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WineShopWebAPI.Models;
using Microsoft.Identity.Web.Resource;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WineShopWebAPI.Authentication;

namespace WineShopWebAPI.Controllers
{
    //[Authorize(Roles = "PurchaseManager")]
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public InventoryController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _context = context;
        }

        // GET: api/Inventory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inventory>>> GetInventories()
        {
            return await _context.Inventories.ToListAsync();
        }

        // GET: api/Inventory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Inventory>> GetInventory(int id)
        {
            var inventory = await _context.Inventories.FindAsync(id);

            if (inventory == null)
            {
                return NotFound();
            }

            return inventory;
        }

        // PUT: api/Inventory/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventory(int id, Inventory inventory)
        {
            if (id != inventory.Inventory_ID)
            {
                return BadRequest();
            }
            var existingProduct = await _context.Products.FindAsync(inventory.Product_ID);
            if (existingProduct == null)
            {
                // If the supplier does not exist, return a 404 Not Found response
                return NotFound($"Supplier with ID {inventory.Product_ID} not found");
            }

            // Assign the existing supplier to the product
            inventory.Product = existingProduct;
            var existingShop = await _context.Shops.FindAsync(inventory.Shop_ID);
            if (existingShop == null)
            {
                // If the supplier does not exist, return a 404 Not Found response
                return NotFound($"Supplier with ID {inventory.Shop_ID} not found");
            }

            // Assign the existing supplier to the product
            inventory.Shop = existingShop;
            _context.Entry(inventory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryExists(id))
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

        // POST: api/Inventory
        [HttpPost]
        public async Task<ActionResult<Inventory>> PostInventory(Inventory inventory)
        {
            var existingProduct = await _context.Products.FindAsync(inventory.Product_ID);
            if (existingProduct == null)
            {
                // If the supplier does not exist, return a 404 Not Found response
                return NotFound($"Supplier with ID {inventory.Product_ID} not found");
            }

            
            var existingShop = await _context.Shops.FindAsync(inventory.Shop_ID);
            if (existingShop == null)
            {
                // If the supplier does not exist, return a 404 Not Found response
                return NotFound($"Supplier with ID {inventory.Shop_ID} not found");
            }

            // Assign the existing supplier to the product
            inventory.Product = existingProduct;
            // Assign the existing supplier to the product
            inventory.Shop = existingShop;
            _context.Inventories.Add(inventory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInventory", new { id = inventory.Inventory_ID }, inventory);
        }

        // DELETE: api/Inventory/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventory(int id)
        {
            var inventory = await _context.Inventories.FindAsync(id);
            if (inventory == null)
            {
                return NotFound();
            }

            _context.Inventories.Remove(inventory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InventoryExists(int id)
        {
            return _context.Inventories.Any(e => e.Inventory_ID == id);
        }



    }
}


