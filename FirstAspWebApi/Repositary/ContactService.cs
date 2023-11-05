using FirstAspWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstAspWebApi.Repositary
{
    public class ContactService:IContactService
    {
        //1 calling Irepositary injection to use its function
        private IRepository<Contact> _Erepository;
        private IRepository<ContactDetail> _detailRepository;

        public ContactService(IRepository<Contact> erepository)
        {
            _Erepository = erepository;
        }

        //2 look Contact Controller 
        //3 edit contactRepository to ContactService
        //also change program.cs contactRepository to ContactService

        public List<Contact> GetAll()
        {
            var useQuery=_Erepository.GetAll();

            return useQuery.Where(contact=>contact.IsActive)
                .Include(con=>con.ContactDetails)
                .ToList();

        }

        //copy code from Controller function
        //recive corresponding query from EntityRep
         public Contact GetById(int id)
        {
            var useQuery = _Erepository.GetById();

           var contact= useQuery.Where(cont => cont.ContactId == id && cont.IsActive==true)
                .Include(con => con.ContactDetails)
                .FirstOrDefault();
            if (contact != null)
            {
                _Erepository.Detached(contact);
            }
            
            return contact;
        }


        public int Add(Contact contact)
        {
           var result= _Erepository.Add(contact);
            return result;
        }


         public Contact Update(Contact updateContact)
        {
            return _Erepository.Update(updateContact);
        }

        public void Delete(Contact contact)
        {
            _Erepository.Delete(contact);
            var contactQuery = _detailRepository.GetById();
            foreach (var contac in contactQuery.Where(con => con.ContactId == contact.ContactId).ToList())
            {
                _detailRepository.Delete(contac);
            }
        }
    }
}
