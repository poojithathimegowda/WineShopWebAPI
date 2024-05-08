﻿using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Admin,User")]
    [ApiController]
    [Route("api/[controller]")]
    public class ExpensesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public ExpensesController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _context = context;
        }

        // GET: api/Expense
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpenses()
        {
            return await _context.Expenses.ToListAsync();
        }

        // GET: api/Expense/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Expense>> GetExpense(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);

            if (expense == null)
            {
                return NotFound();
            }

            return expense;
        }

        // PUT: api/Expense/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpense(int id, ShopExpenses value)
        {
            try
            {
                Expense expense = await _context.Expenses.FindAsync(id);
                if (expense == null)
                {
                    return NotFound();
                }

                expense.Shop_ID = value.Shop_ID;
                expense.Expense_Type = value.Expense_Type;
                expense.Amount = value.Amount;

                var existingShop = await _context.Shops.FindAsync(expense.Shop_ID);
                
                expense.Shop = existingShop;

                _context.Entry(expense).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenseExists(id))
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


        // POST: api/Expense
        [HttpPost]
        public async Task<ActionResult<Expense>> PostExpense(ShopExpenses value)
        {
            try
            {
                Expense expense = new Expense();
                expense.Shop_ID = value.Shop_ID;
                expense.Expense_Type = value.Expense_Type;
                expense.Amount = value.Amount;

                var existingShop = await _context.Shops.FindAsync(value.Shop_ID);
                
                // Assign the existing shop to the expense
                expense.Shop = existingShop;

                _context.Expenses.Add(expense);
                await _context.SaveChangesAsync();

                // Return a 201 Created response with the created expense
                return CreatedAtAction("GetExpense", new { id = expense.Expense_ID }, expense);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        // DELETE: api/Expense/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
            {
                return NotFound();
            }

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExpenseExists(int id)
        {
            return _context.Expenses.Any(e => e.Expense_ID == id);
        }
    }
}
