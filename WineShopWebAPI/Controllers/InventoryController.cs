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
        public async Task<ActionResult<IEnumerable<InventoryJoin>>> GetInventories()
        {
            var inventories = await _context.Inventories
                .Include(i => i.Product)
                .Include(i => i.Shop)
                .Select(i => new InventoryJoin
                {
                    Inventory_ID=i.Inventory_ID,
                    Product_ID = i.Product.Product_ID,
                    Product_Name = i.Product.Product_Name,
                    Shop_ID = i.Shop.Shop_ID,
                    Shop_Name = i.Shop.Shop_Name,
                    Quantity = i.Quantity
                })
                .ToListAsync();

            return inventories;
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
        public async Task<IActionResult> PutInventory(int id, ShopInventory shopInventory)
        {
            try
            {
                Inventory inventory = await _context.Inventories.FindAsync(id);
                if (inventory == null)
                {
                    return NotFound();
                }

                // Update inventory properties with the values from shopInventory
                inventory.Product_ID = shopInventory.Product_ID;
                inventory.Shop_ID = shopInventory.Shop_ID;
                inventory.Quantity = shopInventory.Quantity;

                var existingProduct = await _context.Products.FindAsync(inventory.Product_ID);
                if (existingProduct == null)
                {
                    return NotFound("Product not found");
                }

                var existingShop = await _context.Shops.FindAsync(inventory.Shop_ID);
                if (existingShop == null)
                {
                    return NotFound("Shop not found");
                }

                // Assign the existing product to the inventory
                inventory.Product = existingProduct;
                // Assign the existing shop to the inventory
                inventory.Shop = existingShop;

                _context.Entry(inventory).State = EntityState.Modified;
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
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

            return NoContent();
        }


        // POST: api/Inventory
        [HttpPost]
        public async Task<ActionResult<Inventory>> PostInventory(ShopInventory shopInventory)
        {
            try
            {
                Inventory inventory = new Inventory();
                inventory.Product_ID = shopInventory.Product_ID;
                inventory.Shop_ID = shopInventory.Shop_ID;
                inventory.Quantity = shopInventory.Quantity;

                var existingProduct = await _context.Products.FindAsync(inventory.Product_ID);
                if (existingProduct == null)
                {
                    return NotFound("Product not found");
                }

                var existingShop = await _context.Shops.FindAsync(inventory.Shop_ID);
                if (existingShop == null)
                {
                    return NotFound("Shop not found");
                }

                // Assign the existing product to the inventory
                inventory.Product = existingProduct;
                // Assign the existing shop to the inventory
                inventory.Shop = existingShop;

                _context.Inventories.Add(inventory);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetInventory", new { id = inventory.Inventory_ID }, inventory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
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


