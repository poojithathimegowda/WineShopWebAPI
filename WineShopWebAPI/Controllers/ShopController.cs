using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WineShopWebAPI.Authentication;
using WineShopWebAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;

namespace WineShopWebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ShopController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public ShopController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _context = context;
        }

        // GET: api/Shop
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shop>>> GetShops()
        {
            return await _context.Shops.ToListAsync();
        }

        // GET: api/Shop/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Shop>> GetShop(int id)
        {
            var shop = await _context.Shops.FindAsync(id);

            if (shop == null)
            {
                return NotFound();
            }

            return shop;
        }

        // PUT: api/Shop/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShop(int id, ShopModel shop)
        {
            try
            {
                if (id != shop.Shop_ID)
                {
                    return BadRequest();
                }
                Shop newShop = new Shop()
                {
                    Shop_ID = shop.Shop_ID,
                    Shop_Name = shop.Shop_Name,
                    Location = shop.Location
                };
                _context.Entry(newShop).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShopExists(id))
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

        // POST: api/Shop
        [HttpPost]
        public async Task<ActionResult<Shop>> PostShop(ShopModel shop)
        {
            try
            {
                Shop newShop = new Shop()
                {
                    Shop_ID=shop.Shop_ID,
                    Shop_Name=shop.Shop_Name,
                    Location=shop.Location
                };
                _context.Shops.Add(newShop);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetShop", new { id = shop.Shop_ID }, shop);
            }
            catch (DbUpdateConcurrencyException)
            {

                throw;

            }

        }

        // DELETE: api/Shop/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShop(int id)
        {
            var shop = await _context.Shops.FindAsync(id);
            if (shop == null)
            {
                return NotFound();
            }

            _context.Shops.Remove(shop);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShopExists(int id)
        {
            return _context.Shops.Any(e => e.Shop_ID == id);
        }


        // GET: api/Shop/AutoComplete
        [HttpGet("AutoComplete")]
        public IActionResult AutoComplete(string term)
        {
            try
            {
                // Query the database for shops that match the term
                var shops = _context.Shops
                    .Where(s => s.Shop_Name.Contains(term))
                    .Select(s => new { label = s.Shop_Name, value = s.Shop_ID })
                    .ToList();

                // Return the JSON result
                return Ok(shops);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("GetProfitAndLoss")]
        public async Task<ActionResult<ProfitLossDto>> GetProfitAndLoss(int shopId, DateTime start, DateTime end)
        {
            // Validate the input dates
            if (start >= end)
            {
                return BadRequest("Start date must be earlier than end date.");
            }

            // Fetch and fill shop obj

            var shop= await _context.Shops.Where(s=>s.Shop_ID== shopId).FirstOrDefaultAsync();

            // Fetch and group orders by date
            var orders = await _context.Orders
                .Where(o => o.Shop_ID == shopId && o.Order_Date >= start && o.Order_Date <= end)
                .GroupBy(o => o.Order_Date.Date)
                .Select(g => new OrderDto
                {
                    Date = g.Key,
                    TotalAmount = g.Sum(o => o.Total_Amount)
                })
                .ToListAsync();

            // Fetch and group expenses by date
            var expenses = await _context.Expenses
                .Where(e => e.Shop_ID == shopId && e.Date >= start && e.Date <= end)
                .GroupBy(e => e.Date.Date)
                .Select(g => new ExpenseDto
                {
                    Date = g.Key,
                    Amount = g.Sum(e => e.Amount)
                })
                .ToListAsync();

            // Calculate total income and expenses
            var totalIncome = orders.Sum(o => o.TotalAmount);
            var totalExpenses = expenses.Sum(e => e.Amount);
            var profitOrLoss = totalIncome - totalExpenses;

            // Create the result
            var result = new ProfitLossDto
            {
                Shop = new ShopModel() {
                    Shop_ID = shop.Shop_ID,
                    Shop_Name=shop.Shop_Name,
                    Location=shop.Location
                },
                Income = totalIncome,
                Expenses = totalExpenses,
                ProfitOrLoss = profitOrLoss,
                Orders = orders,
                ExpensesList = expenses
            };

            return Ok(result);
        }

    }
}
