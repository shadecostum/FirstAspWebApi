using FirstAspWebApi.Repositary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstAspWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
       // private readonly ContactRepo _contactRepo;

        private readonly IContactRepo _contactRepo;

        public ContactsController(IContactRepo contactRepo)
        {
            _contactRepo = contactRepo;
        }


        [HttpGet("getControllData")]

        public IActionResult Get()
        {
            var contactData= _contactRepo.GetAllData();

            if (contactData.Count!=0)
            {
                return Ok(contactData);
            }
            return BadRequest("Erro 400");
        }

        [HttpGet("getById/{id:int}")]
        public IActionResult Get(int id)
        {
            var contactId=_contactRepo.GetContactById(id);

            if(contactId!=null)
            {
                return Ok(contactId);
            }
            return BadRequest("ID not found try ");
        }

    }
}
