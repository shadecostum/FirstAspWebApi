using FirstAspWebApi.Data;
using FirstAspWebApi.DTO;
using FirstAspWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

   
        public int AddDetails(ContactDetail detail)
        {
          _context.Details.Add(detail);
            _context.SaveChanges();

            var detailsId=_context.Details.Where(det=>det.Type == detail.Type)
                .OrderBy(det=>det.DetailId).LastOrDefault().DetailId;

           return detailsId;
        }


        //public ContactDetail UpdateDetails(ContactDetail detail,ContactDetail oldDetails)
        //{
        //    var matchId = GetUserById(detail.DetailId);
        //    if (matchId != null)
        //    {
        //        _context.Entry(oldDetails).State = EntityState.Detached;
        //        _context.Details.Update(detail);
        //        _context.SaveChanges();
        //        return matchId;
        //    }
        //    return null;
        //}


        public bool DeletDetails(int id)
        {
            var matchId= GetUserById(id);
            if (matchId != null)
            {
                _context.Details.Remove(matchId);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
