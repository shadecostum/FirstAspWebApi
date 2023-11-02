using FirstAspWebApi.Models;

namespace FirstAspWebApi.Repositary
{
    //changing to service 
    public interface IContactService
    {
        public List<Contact> GetAll();

        public Contact GetById(int id);


        public int Add(Contact contact);

        public Contact Update(Contact updateContact);

        public void Delete(Contact contact);
    }
}
