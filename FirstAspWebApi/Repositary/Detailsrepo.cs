using FirstAspWebApi.Data;
using FirstAspWebApi.Models;

namespace FirstAspWebApi.Repositary
{
    public class Detailsrepo:IDetailsRepo
    {
        private readonly MyContext _context;

        public Detailsrepo(MyContext context)
        {
            _context = context;
        }


       
        public List<ContactDetail> GetAllData()
        {
            return _context.Details.ToList();
        }

        public ContactDetail GetUserById(int id)
        {
            return _context.Details.Where(dept => dept.DetailId == id).FirstOrDefault();
        }
    }
}
