using FirstAspWebApi.DTO;
using FirstAspWebApi.Models;
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


        //showing dto with all data

        private UserDto ConvertToDto(User user)
        {
            return new UserDto()
            {
                userId = user.userId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IsAdmin = user.IsAdmin,
                IsActive = user.IsActive,
                CountContacts = user.Contacts != null ? user.Contacts.Count() : 0
            };
        }


        [HttpGet("getAll")]

        public IActionResult Get()
        {
            List<UserDto> usersDtoList = new List<UserDto>();//list showing


            var usersData = _userRepo.GetAllData();

            if(usersData.Count==0)
            {
                return BadRequest("No user added yet");
            }
            else
            {
                foreach (var user in usersData)
                {
                    usersDtoList.Add(ConvertToDto(user));
                }
            }
            return Ok(usersDtoList);
        }

        [HttpGet("getById/{id:int}")]
        public IActionResult Get(int id)
        {
            var userId=_userRepo.GetUserById(id);
            if(userId!=null)
            {
               var userDto=ConvertToDto(userId);
                return Ok(userDto);
            }
            return NotFound("No such uwser Exist");
        }

        private User ConvertToModel(UserDto userDto)
        {
            return new User()
            {
                userId=userDto.userId,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                IsAdmin = userDto.IsAdmin,
                IsActive = userDto.IsActive,

            };
        }

        [HttpPost("add")]
        public IActionResult Post(UserDto userDto)
        {

            var user = ConvertToModel(userDto);


            var adduser=_userRepo.Add(user);

            if(adduser!=null)
            {
                return Ok(adduser);
            }
            return BadRequest("No data added");
        }

        [HttpPut("update")]
        public IActionResult Update(UserDto userDto)
        {
            var user = _userRepo.GetUserById(userDto.userId);//validation

            if(user!=null)
            {
                //note changes then look userRep
                var updatedUser = ConvertToModel(userDto);
                var modifiedUser = _userRepo.Update(updatedUser);
                return Ok(modifiedUser);
            }
            return BadRequest("Some issue updating");
        }

        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            var user= _userRepo.GetUserById(id);//validation added

            if(user!=null)
            {
                _userRepo.Delete(user);//show error
                return Ok(id);
            }
            return BadRequest("No user Deleted");
        }
    }
}
