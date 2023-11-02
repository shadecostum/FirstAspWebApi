using FirstAspWebApi.Models;

namespace FirstAspWebApi.Repositary
{
    public interface IUserService
    {
        public List<User> GetAll();
        public User GetById(int id);
        public int Add(User user);

        public User Update(User user);

        public void Delete(User user);
    }
}
