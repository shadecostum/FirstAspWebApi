using FirstAspWebApi.Models;

namespace FirstAspWebApi.Repositary
{
    public interface IDetailsRepo
    {
        public List<ContactDetail> GetAllData();

        public ContactDetail GetUserById(int id);
    }
}
