using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WineShopWebAPI.Authentication;
using WineShopWebAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WineShopWebAPI.Controllers
{
    //[Authorize(Roles = "Admin,User")]
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public AdminController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _context = context;
        }

        [HttpPost]
        [Route("register-PurchaseManager")]
        public async Task<IActionResult> RegisterPurchaseManager([FromForm] RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (!await roleManager.RoleExistsAsync(UserRoles.PurchaseManager))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.PurchaseManager));

            if (await roleManager.RoleExistsAsync(UserRoles.PurchaseManager))
            {
                await userManager.AddToRoleAsync(user, UserRoles.PurchaseManager);
            }

            return Ok(new Response { Status = "Success", Message = "Purchase Manager created successfully!" });
        }
        [HttpPost]
        [Route("register-SalesRep")]
        public async Task<IActionResult> RegisterSalesRep([FromForm] RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (!await roleManager.RoleExistsAsync(UserRoles.SalesRep))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.SalesRep));
            if (!await roleManager.RoleExistsAsync(UserRoles.User))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await roleManager.RoleExistsAsync(UserRoles.SalesRep))
            {
                await userManager.AddToRoleAsync(user, UserRoles.SalesRep);
            }

            return Ok(new Response { Status = "Success", Message = "SalesRep user created successfully!" });
        }
        /// <summary>
        /// Retrieves all shops.
        /// </summary>
        [HttpGet("shops")]
        public async Task<ActionResult<IEnumerable<Shop>>> GetAllShops()
        {
            return await _context.Shops.ToListAsync();
        }

        /// <summary>
        /// Retrieves a specific shop by its ID.
        /// </summary>
        [HttpGet("shops/{id}")]
        public async Task<ActionResult<Shop>> GetShopById(int id)
        {
            var shop = await _context.Shops.FindAsync(id);

            if (shop == null)
            {
                return NotFound();
            }

            return shop;
        }

        /// <summary>
        /// Creates a new shop.
        /// </summary>
        [HttpPost("shops")]
        public async Task<ActionResult<Shop>> CreateShop(Shop shop)
        {
            _context.Shops.Add(shop);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetShopById), new { id = shop.Id }, shop);
        }

        /// <summary>
        /// Updates an existing shop.
        /// </summary>
        [HttpPut("shops/{id}")]
        public async Task<IActionResult> UpdateShop(int id, Shop shop)
        {
            if (id != shop.Id)
            {
                return BadRequest();
            }

            _context.Entry(shop).State = EntityState.Modified;

            try
            {
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

        /// <summary>
        /// Deletes a shop by its ID.
        /// </summary>
        [HttpDelete("shops/{id}")]
        public async Task<IActionResult> DeleteShopById(int id)
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
            return _context.Shops.Any(e => e.Id == id);
        }

        // GET: api/admin/accountgroups
        [HttpGet("accountgroups")]
        public ActionResult<IEnumerable<AccountGroup>> GetAccountGroups()
        {
            return _context.AccountGroups.ToList();
        }

        // GET: api/admin/accountgroups/{id}
        [HttpGet("accountgroups/{id}")]
        public ActionResult<AccountGroup> GetAccountGroup(int id)
        {
            var accountGroup = _context.AccountGroups.Find(id);

            if (accountGroup == null)
            {
                return NotFound();
            }

            return accountGroup;
        }

        // POST: api/admin/accountgroups
        [HttpPost("accountgroups")]
        public ActionResult<AccountGroup> PostAccountGroup(AccountGroup accountGroup)
        {
            
            _context.AccountGroups.Add(accountGroup);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetAccountGroup), new { id = accountGroup.Id }, accountGroup);
        }

        // PUT: api/admin/accountgroups/{id}
        [HttpPut("accountgroups/{id}")]
        public IActionResult PutAccountGroup(int id, AccountGroup accountGroup)
        {
            if (id != accountGroup.Id)
            {
                return BadRequest();
            }

            _context.Entry(accountGroup).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/admin/accountgroups/{id}
        [HttpDelete("accountgroups/{id}")]
        public IActionResult DeleteAccountGroup(int id)
        {
            var accountGroup = _context.AccountGroups.Find(id);

            if (accountGroup == null)
            {
                return NotFound();
            }

            _context.AccountGroups.Remove(accountGroup);
            _context.SaveChanges();

            return NoContent();
        }


        // GET: api/Admin
        [HttpGet("chartofaccounts")]
        public async Task<ActionResult<IEnumerable<ChartOfAccount>>> GetChartOfAccounts()
        {
            return await _context.ChartOfAccounts.ToListAsync();
        }

        // GET: api/Admin/5
        [HttpGet("chartofaccounts/{id}")]
        public async Task<ActionResult<ChartOfAccount>> GetChartOfAccount(int id)
        {
            var chartOfAccount = await _context.ChartOfAccounts.FindAsync(id);

            if (chartOfAccount == null)
            {
                return NotFound();
            }

            return chartOfAccount;
        }

        // PUT: api/Admin/5
        [HttpPut("chartofaccounts/{id}")]
        public async Task<IActionResult> PutChartOfAccount(int id, ChartOfAccount chartOfAccount)
        {
            if (id != chartOfAccount.Id)
            {
                return BadRequest();
            }

            _context.Entry(chartOfAccount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChartOfAccountExists(id))
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

        // POST: api/Admin
        [HttpPost("chartofaccounts")]
        public async Task<ActionResult<ChartOfAccount>> PostChartOfAccount(ChartOfAccount chartOfAccount)
        {
            _context.ChartOfAccounts.Add(chartOfAccount);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetChartOfAccount), new { id = chartOfAccount.Id }, chartOfAccount);
        }

        // DELETE: api/Admin/5
        [HttpDelete("chartofaccounts/{id}")]
        public async Task<IActionResult> DeleteChartOfAccount(int id)
        {
            var chartOfAccount = await _context.ChartOfAccounts.FindAsync(id);
            if (chartOfAccount == null)
            {
                return NotFound();
            }

            _context.ChartOfAccounts.Remove(chartOfAccount);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChartOfAccountExists(int id)
        {
            return _context.ChartOfAccounts.Any(e => e.Id == id);
        }


        // GET: api/Admin/Suppliers
        [HttpGet("Suppliers")]
        public async Task<ActionResult<IEnumerable<Supplier>>> GetSuppliers()
        {
            return await _context.Suppliers.ToListAsync();
        }

        // GET: api/Admin/Suppliers/5
        [HttpGet("Suppliers/{id}")]
        public async Task<ActionResult<Supplier>> GetSupplier(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);

            if (supplier == null)
            {
                return NotFound();
            }

            return supplier;
        }

        // PUT: api/Admin/Suppliers/5
        [HttpPut("Suppliers/{id}")]
        public async Task<IActionResult> PutSupplier(int id, Supplier supplier)
        {
            if (id != supplier.Id)
            {
                return BadRequest();
            }

            _context.Entry(supplier).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupplierExists(id))
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

        // POST: api/Admin/Suppliers
        [HttpPost("Suppliers")]
        public async Task<ActionResult<Supplier>> PostSupplier(Supplier supplier)
        {
            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSupplier), new { id = supplier.Id }, supplier);
        }

        // DELETE: api/Admin/Suppliers/5
        [HttpDelete("Suppliers/{id}")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }

            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SupplierExists(int id)
        {
            return _context.Suppliers.Any(e => e.Id == id);
        }



        // GET: api/Admin
        [HttpGet("Customer")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        // GET: api/Admin/5
        [HttpGet("Customer/{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Admin/5
        [HttpPut("Customer/{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        // POST: api/Admin
        [HttpPost("Customer")]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
        }

        // DELETE: api/Admin/5
        [HttpDelete("Customer/{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }

    }
}
