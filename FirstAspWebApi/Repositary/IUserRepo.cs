using FirstAspWebApi.Models;

namespace FirstAspWebApi.Repositary
{
    public interface IUserRepo
    {
        public List<User> GetAllData();

        public User GetUserById(int id);

        public int Add(User user);

        // public User Update(User user);
        public User Update(User updateUser);

        // public bool Delete(int id);
        public void Delete(User user);
        public void DeleteAll(User user);

    }


}
