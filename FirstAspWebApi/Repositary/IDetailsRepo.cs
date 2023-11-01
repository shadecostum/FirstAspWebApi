using FirstAspWebApi.Models;

namespace FirstAspWebApi.Repositary
{
    public interface IDetailsRepo
    {
        public List<ContactDetail> GetAllData();

        public ContactDetail GetUserById(int id);

        public int AddDetails(ContactDetail detail);

        public ContactDetail UpdateDetails(ContactDetail detail);
       // public ContactDetail UpdateDetails(ContactDetail detail, ContactDetail oldDetails);
        public bool DeletDetails(int id);
    }
}
