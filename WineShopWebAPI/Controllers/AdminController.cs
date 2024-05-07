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
    [Authorize(Roles = "Admin,User")]
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public AdminController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _context = context;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Registration([FromBody] RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            User user = new User()
            {
                UserName = model.Username,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                // Set the Name property to the full name provided by the user
                Name = model.FirstName + " " + model.LastName, // Assuming FirstName and LastName properties are available in RegisterModel,
                Role = model.Role
            };




            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            // Check if the role exists
            if (!await roleManager.RoleExistsAsync(model.Role))
                await roleManager.CreateAsync(new IdentityRole(model.Role));

            // Add user to the specified role
            if (await roleManager.RoleExistsAsync(model.Role))
            {
                await userManager.AddToRoleAsync(user, model.Role);

                // For Admin and Store Manager roles, check if Shop_ID is provided and update the user's Shop_ID
                if (model.Role == "Admin" || model.Role == "StoreManager")
                {
                    //if (model.Shop_ID.HasValue)
                   // {
                        // Update the user's Shop_ID
                        user.Shop_ID = model.Shop_ID;
                        await userManager.UpdateAsync(user);
                    ////}
                    ////else
                    ////{
                    //    // Return error if Shop_ID is not provided for Admin or Store Manager
                    //    return BadRequest(new Response { Status = "Error", Message = "Shop_ID is required for Admin or Store Manager role!" });
                    //}
                }
            }

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }





    }
}
