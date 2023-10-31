using FirstAspWebApi.Data;
using FirstAspWebApi.Models;

namespace FirstAspWebApi.Repositary
{
    public class UserRepo:IUserRepo
    {
        private readonly MyContext _context;

        public UserRepo(MyContext context)
        {
            _context = context;
        }


        public List<User> GetAllData()
        {
            return _context.users
                .Where(user=>user.IsActive==true)
                .ToList();
        }

        public User GetUserById(int id)
        {
            return _context.users.Where(user => user.userId == id && user.IsActive == true).FirstOrDefault();
        }

    }
}
