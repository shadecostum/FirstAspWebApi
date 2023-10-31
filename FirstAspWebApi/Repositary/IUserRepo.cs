using FirstAspWebApi.Models;

namespace FirstAspWebApi.Repositary
{
    public interface IUserRepo
    {
        public List<User> GetAllData();

        public User GetUserById(int id);
    }


}
