using FirstAspWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstAspWebApi.Repositary
{
    public class UserService:IUserService
    {

        private IRepository<User> _repository;

        public UserService(IRepository<User> repository)
        {
            _repository = repository;
        }

        public List<User> GetAll()
        {
            var userQuery=_repository.GetAll();

            return  userQuery.Where(User=>User.IsActive)
                .Include(User=>User.Contacts.Where(contact=>contact.IsActive ==true))
                .ToList();
        }
        //public List<User> GetAll()
        //{
        //    string[] includetables = { "Contacts" };
        //    var users=_repository.GetAll(includetables);
        //    return users.Where(user=>user.IsActive).ToList();//using high data is not good old way
        //}

        public  User GetById(int id)
        {
           var useQuery=_repository.GetById();

            var user= useQuery.Where(User => User.userId == id && User.IsActive == true)
                .Include(usr => usr.Contacts    )
                .FirstOrDefault();
            if(user!=null)
            {
                _repository.Detached(user);
            }
            return user;
        }

        public int Add(User user)
        {
            return _repository.Add(user);
        }

        public User Update(User user)
        {
            return _repository.Update(user);
        }

        public void Delete(User user)
        {
            _repository.Delete(user);
        }





    }
}
