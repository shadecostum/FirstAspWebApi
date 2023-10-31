using FirstAspWebApi.Models;

namespace FirstAspWebApi.Repositary
{
    public interface IContactRepo
    {
        public List<Contact> GetAllData();

        public Contact GetContactById(int id);
    }
}
