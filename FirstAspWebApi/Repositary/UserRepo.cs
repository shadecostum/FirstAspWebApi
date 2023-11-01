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

        //return list usermodel data  from table data feteched 
        //fetching data soft delete true only we use where
        //if need other table count needed we use Include call table check isActive 
        //toList must added cause returning list
        public List<User> GetAllData()
        {
            return _context.users
                .Where(user=>user.IsActive==true)
                .Include(user=>user.Contacts.Where(use=>use.IsActive==true))
                .ToList();

           
        }



        //using table matched id check and also check is active 
        //taking first 
        //use some detached funct to untarck user used for readonly state making cannot update
        public User GetUserById(int id)
        {
            var user= _context.users.Where(user => user.userId == id && user.IsActive == true).FirstOrDefault();
            if (user != null)
            {
                _context.Entry(user).State = EntityState.Detached;
            }
            return user;
        }



        //1 user data recived conn.table.inbuildAdd functiom passed
        //2 save changes
        //3 user recived fun alredy added so check id passed user.name==table.name match it
        //4 orderby id last added id return 
        public int  Add(User user)
        {
            _context.users.Add(user);
            _context.SaveChanges();  

            var newUserId=_context.users.Where(userr=>userr.FirstName==user.FirstName)
                .OrderBy(usr=>usr.userId).LastOrDefault().userId;
            return newUserId;
        }



        //here user data passed 
        //1 inbuild table .update pass userdata
        //save changes 
        //return updated data
        public User Update(User updateUser)//here code changed validation copy in Interface also
        {
                _context.users.Update(updateUser);
                _context.SaveChanges() ;
                return updateUser;
        }

        //made void 
        //modified inbuild user data retrive  for modification used enity 
        //set isActive =false
        //if you also want to connected table set isActive false 
        //foreach used another table data checking matched 
        //set isActive=false
        // save changes
        public void Delete(User user)//change in interface
        {
            _context.Entry(user) .State = EntityState.Modified;
                user.IsActive = false;
          foreach(var contact in _context.contacts.Where(userr=>userr.UserId==user.userId))
                    contact.IsActive = false;
          _context.SaveChanges();
            
         
        }

    }
}
