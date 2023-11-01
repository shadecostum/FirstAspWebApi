using FirstAspWebApi.Data;
using FirstAspWebApi.Models;
using Microsoft.EntityFrameworkCore;

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
                .Include(user=>user.Contacts.Where(use=>use.IsActive==true))
                .ToList();

           
        }

        public User GetUserById(int id)
        {
            var user= _context.users.Where(user => user.userId == id && user.IsActive == true).FirstOrDefault();
            if (user != null)
            {
                _context.Entry(user).State = EntityState.Detached;
            }
            return user;
        }

        public int  Add(User user)
        {
            _context.users.Add(user);
            _context.SaveChanges();  

            var newUserId=_context.users.Where(userr=>userr.FirstName==user.FirstName)
                .OrderBy(usr=>usr.userId).LastOrDefault().userId;
            return newUserId;
        }

        public User Update(User updateUser)//here code changed validation copy in Interface also
        {
           // var userId = GetUserById(user.userId);
           // if (userId != null)
            
               // _context.Entry(oldUser).State = EntityState.Detached; //applied in getByid
                _context.users.Update(updateUser);
                _context.SaveChanges() ;
                return updateUser;
            
           
        }

        //made void 
        public void Delete(User user)//change in interface
        {
           

            _context.Entry(user) .State = EntityState.Modified;
                user.IsActive = false;
            
                _context.SaveChanges();
            
         
        }

    }
}
