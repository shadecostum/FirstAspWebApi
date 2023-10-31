using FirstAspWebApi.Data;
using FirstAspWebApi.Models;

namespace FirstAspWebApi.Repositary
{
    public class ContactRepo:IContactRepo
    {
        private readonly MyContext _context;

        public ContactRepo(MyContext context)
        {
            _context = context;
        }


        public List<Contact> GetAllData()
        {
            return _context.contacts
                .Where(cont=>cont.IsActive==true)
                .ToList();
        }

        public Contact GetContactById(int id)
        {
           return _context.contacts.Where(cont1=>cont1.ContactId==id && cont1.IsActive==true)
                .FirstOrDefault();
            
        }
    }
}
