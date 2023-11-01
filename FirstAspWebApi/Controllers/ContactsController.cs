using FirstAspWebApi.DTO;
using FirstAspWebApi.Models;
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

        private ContactDto ConvertToDto(Contact contact)
        {
            return new ContactDto()
            {
                ContactId = contact.ContactId,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                IsActive = contact.IsActive,
                UserId = contact.UserId,
               CountDetails = contact.ContactDetails != null? contact.ContactDetails.Count() : 0
            };
        }


        [HttpGet("getControllData")]

        public IActionResult Get()
        {
           List<ContactDto> contactsList = new List<ContactDto>();

            var contactData= _contactRepo.GetAllData();

            if (contactData.Count==0)
            {
                return BadRequest("Erro 400  getting all");
            }
            else
            {
                foreach (var contact in contactData)
                {
                    contactsList.Add(ConvertToDto(contact));
                }
            }
            return Ok(contactsList);
            
        }

        [HttpGet("getById/{id:int}")]
        public IActionResult Get(int id)
        {
            var contactId=_contactRepo.GetContactById(id);

            if(contactId!=null)
            {
                var ContactDto = ConvertToDto(contactId);
                return Ok(ContactDto);
            }
            return BadRequest("ID not found try ");
        }


       private Contact  ConvertToModel(ContactDto contactDto)
        {
            return new Contact
            {
                ContactId = contactDto.ContactId,
                FirstName = contactDto.FirstName,
                LastName = contactDto.LastName,
                IsActive = contactDto.IsActive,
                UserId = contactDto.UserId,
            };
        }

        [HttpPost("add")]
       public IActionResult Post(ContactDto contactDto)
        {
            var contact=ConvertToModel(contactDto);

            var addContact = _contactRepo.Add(contact);

            if (addContact != null)
            {
                return Ok(addContact);
            }
            return BadRequest("Contacts Nod added");
        }

        [HttpPut("update")]
        public IActionResult Put(ContactDto contactDto)
        {
           
           var contactId=_contactRepo.GetContactById(contactDto.ContactId);
            if(contactId!=null)
            {
                var updateContact = ConvertToModel(contactDto);
                // var modifiedUser = _userRepo.Update(updatedUser, user);
                var modifiedContact = _contactRepo.Update(updateContact);//first pass controller Update
                return Ok(modifiedContact);
            }

            //if(modifiedContact != null)
            //{
            //   return Ok(modifiedContact);
            //}
            return NotFound("Not updated ");
        }

        [HttpDelete("delete")]//do nothing with Dto
        public IActionResult Delete(int id)
        {
            var contactData= _contactRepo.GetContactById(id);

            if(contactData!=null)
            {
                _contactRepo.Delete(contactData);
                return Ok(id);
            }
            return BadRequest("Error in delete request");

            //bool isRemoved = _contactRepo.Delete(id);
            //if (isRemoved)
            //{
            //    return Ok(id);
            //}
            //return BadRequest("No user Deleted");
        }

    }
}
