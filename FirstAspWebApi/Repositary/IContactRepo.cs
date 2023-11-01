using FirstAspWebApi.Models;

namespace FirstAspWebApi.Repositary
{
    public interface IContactRepo
    {
        public List<Contact> GetAllData();

        public Contact GetContactById(int id);
        public int Add(Contact Contact);

        public Contact Update(Contact updatedContact, Contact oldContact);

        public void Delete(Contact contact);//void changed
    }
}
