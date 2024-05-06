//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using WineShopWebAPI.Models;
//using Microsoft.Identity.Web.Resource;
//using WineShopWebAPI.Authentication;
//using Microsoft.EntityFrameworkCore;

//namespace WineShopWebAPI.Controllers
//{
//    [Authorize(Roles = "SalesRep")]
//    [ApiController]
//    [Route("api/[controller]")]
//    public class SalesRepController : ControllerBase
//    {
//        private readonly ApplicationDbContext _context;
//        public SalesRepController(ApplicationDbContext context)
//        {

//            _context = context;
//        }

//        // GET: api/SalesRep/SalesRegisters
//        [HttpGet("SalesRegisters")]
//        public async Task<ActionResult<IEnumerable<SalesRegister>>> GetSalesRegisters()
//        {
//            return await _context.SalesRegisters.Include(s => s.Customer).Include(s => s.Item).ToListAsync();
//        }

//        // GET: api/SalesRep/SalesRegisters/5
//        [HttpGet("SalesRegisters/{id}")]
//        public async Task<ActionResult<SalesRegister>> GetSalesRegister(int id)
//        {
//            var salesRegister = await _context.SalesRegisters.FindAsync(id);

//            if (salesRegister == null)
//            {
//                return NotFound();
//            }

//            return salesRegister;
//        }

//        // PUT: api/SalesRep/SalesRegisters/5
//        [HttpPut("SalesRegisters/{id}")]
//        public async Task<IActionResult> PutSalesRegister(int id, SalesRegister salesRegister)
//        {
//            if (id != salesRegister.Id)
//            {
//                return BadRequest();
//            }

//            _context.Entry(salesRegister).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!SalesRegisterExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // POST: api/SalesRep/SalesRegisters
//        [HttpPost("SalesRegisters")]
//        public async Task<ActionResult<SalesRegister>> PostSalesRegister(SalesRegister salesRegister)
//        {
//            _context.SalesRegisters.Add(salesRegister);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction(nameof(GetSalesRegister), new { id = salesRegister.Id }, salesRegister);
//        }

//        // DELETE: api/SalesRep/SalesRegisters/5
//        [HttpDelete("SalesRegisters/{id}")]
//        public async Task<IActionResult> DeleteSalesRegister(int id)
//        {
//            var salesRegister = await _context.SalesRegisters.FindAsync(id);
//            if (salesRegister == null)
//            {
//                return NotFound();
//            }

//            _context.SalesRegisters.Remove(salesRegister);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool SalesRegisterExists(int id)
//        {
//            return _context.SalesRegisters.Any(e => e.Id == id);
//        }



//        // GET: api/SalesRep/ExpenseClaims
//        [HttpGet("ExpenseClaims")]
//        public async Task<ActionResult<IEnumerable<ExpenseClaim>>> GetExpenseClaims()
//        {
//            return await _context.ExpenseClaims.Include(ec => ec.ChartOfAccount).Include(ec => ec.Customer).ToListAsync();
//        }

//        // GET: api/SalesRep/ExpenseClaims/5
//        [HttpGet("ExpenseClaims/{id}")]
//        public async Task<ActionResult<ExpenseClaim>> GetExpenseClaim(int id)
//        {
//            var expenseClaim = await _context.ExpenseClaims.Include(ec => ec.ChartOfAccount).Include(ec => ec.Customer).FirstOrDefaultAsync(ec => ec.Id == id);

//            if (expenseClaim == null)
//            {
//                return NotFound();
//            }

//            return expenseClaim;
//        }

//        // PUT: api/SalesRep/ExpenseClaims/5
//        [HttpPut("ExpenseClaims/{id}")]
//        public async Task<IActionResult> PutExpenseClaim(int id, ExpenseClaim expenseClaim)
//        {
//            if (id != expenseClaim.Id)
//            {
//                return BadRequest();
//            }

//            _context.Entry(expenseClaim).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!ExpenseClaimExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // POST: api/SalesRep/ExpenseClaims
//        [HttpPost("ExpenseClaims")]
//        public async Task<ActionResult<ExpenseClaim>> PostExpenseClaim(ExpenseClaim expenseClaim)
//        {
//            _context.ExpenseClaims.Add(expenseClaim);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction(nameof(GetExpenseClaim), new { id = expenseClaim.Id }, expenseClaim);
//        }

//        // DELETE: api/SalesRep/ExpenseClaims/5
//        [HttpDelete("ExpenseClaims/{id}")]
//        public async Task<IActionResult> DeleteExpenseClaim(int id)
//        {
//            var expenseClaim = await _context.ExpenseClaims.FindAsync(id);
//            if (expenseClaim == null)
//            {
//                return NotFound();
//            }

//            _context.ExpenseClaims.Remove(expenseClaim);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool ExpenseClaimExists(int id)
//        {
//            return _context.ExpenseClaims.Any(e => e.Id == id);
//        }



//    }
//}


