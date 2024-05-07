using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WineShopWebAPI.Models;
using Microsoft.Identity.Web.Resource;
using WineShopWebAPI.Authentication;
using Microsoft.EntityFrameworkCore;

namespace WineShopWebAPI.Controllers
{
    [Authorize(Roles = "SalesRep")]
    [ApiController]
    [Route("api/[controller]")]
    public class SalesRepController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public SalesRepController(ApplicationDbContext context)
        {

            _context = context;
        }

      



    }
}


