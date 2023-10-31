using FirstAspWebApi.Repositary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstAspWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepo _userRepo;

        public UsersController(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet("getAll")]

        public IActionResult Get()
        {
            var usersData = _userRepo.GetAllData();

            if(usersData.Count==0)
            {
                return BadRequest("No user added yet");
            }
            return Ok(usersData);
        }

        [HttpGet("getById/{id:int}")]
        public IActionResult Get(int id)
        {
            var userId=_userRepo.GetUserById(id);
            if(userId!=null)
            {
                return Ok(userId);
            }
            return NotFound("No such uwser Exist");
        }
    }
}
