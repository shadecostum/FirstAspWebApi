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


        //used to user data passed it convert to Dto model so it used to show specific properties only

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
        //list showing as Dto Model Class Contain
        //so 1 create a list Dto data to show 
        //2 call rep fun it retur user table entire data
        //so we need to show dto spcific data
        //3 checking return any data contain return badrequest
        //4 foreach user data convert to Dto by created Dto list.Add each user data
        //5 return to list to show
        public IActionResult Get()
        {
            List<UserDto> usersDtoList = new List<UserDto>();          
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



        //1 using id search data call rep fun id check
        //2 recive readonly state od specific user data
        //check data get convert to Dto or notfound
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


        //dto class initialized properties no needed to fill strictly..... only if [required] set enter strictly 
        //by adding entire tables connected so we need to added all data
        //using Dto recive data and convert to user model data specific user data only store


        //1 reciving Dto value,function argumnet set as Dto
        //2 covert Dto to user by private function passing return user data
        //3 repo func add called and variable passed
        //4 so user added id return  show Ok()
        //id not return badrequest
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




        //1 here also dto data argument set 
        //2 pass dto id match to any value also detached the id 
        //3 retrun matched user data
        //4 we have Dto data so it convert to user data
        //5 converted data passed to fun update
        //
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



        //take id from user
        //id passed to repo searchid fun  it detached and return full matched data
        //call repo delet fun writen pass matched data
        //retrn id if matched condition inside
        //else bad request
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
