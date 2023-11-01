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
            return _context.Details.ToList()
                ;
        }

        public ContactDetail GetUserById(int id)
        {
            //checking is Active customer
            var detail= _context.Details.Where(dept => dept.DetailId == id).FirstOrDefault();
            if (detail != null)
            {
                _context.Entry(detail).State = EntityState.Detached;
            }
            return detail;
        }

   
        public int AddDetails(ContactDetail detail)
        {
          _context.Details.Add(detail);
            _context.SaveChanges();

            var detailsId=_context.Details.Where(det=>det.Type == detail.Type)
                .OrderBy(det=>det.DetailId).LastOrDefault().DetailId;

           return detailsId;
        }


        //public ContactDetail UpdateDetails(ContactDetail detail)
        //{
        //    _context.Details.Update(detail);
        //    _context.SaveChanges();
        //    return detail;


        //   // var matchId = GetUserById(detail.DetailId);
        //    //if (matchId != null)
        //    //{
        //    //    _context.Entry(oldDetails).State = EntityState.Detached;
        //    //    _context.Details.Update(detail);
        //    //    _context.SaveChanges();
        //    //    return matchId;
        //    //}
        //    //return null;
        //}

        public ContactDetail UpdateDetails(ContactDetail detail)
        {
         //_context.Entry(oldDetails).State = EntityState.Detached;
            _context.Details.Update(detail);//inbuild update
            _context.SaveChanges();
            return detail;
        }


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
