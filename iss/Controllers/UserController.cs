using Microsoft.AspNetCore.Mvc;
using iss.Data;
using iss.Models;
using Microsoft.AspNetCore.Cors;

namespace iss.Controllers
{
    [EnableCors("ISSPOLICY")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("create")]
        public dynamic CreateUser([FromBody] User user)
        {
            return _userService.CreateUser(user);
        }

        [HttpGet]
        [Route("auth/{idnumber}")]
        public dynamic? AuthUser(string idnumber)
        {
            try
            {
                return _userService.AuthUser(idnumber);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet]
        [Route("get")]
        public dynamic? GetUser(int id)
        {
            try
            {
                return _userService.GetUser(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
