using System.Collections.Generic;
using System.Linq;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;
        }

        //hämtar alla users, enpoint: api/users
        [HttpGet]
        public ActionResult<IEnumerable<AppUser>> GetUsers()
        {
            return _context.Users.ToList();
        }

        //hämtar en user på id, endpoint api/users/id
        [HttpGet("{id}")]
        public ActionResult<AppUser> GetUser(int id) 
        {
            return _context.Users.Find(id);
        }
    }
}