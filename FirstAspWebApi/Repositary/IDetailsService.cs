using FirstAspWebApi.Models;
using System;

namespace FirstAspWebApi.Repositary
{
    public interface IDetailsService
    {
        public List<ContactDetail> GetAll();

        public ContactDetail GetById(int id);

        public int Add(ContactDetail detail);

        public ContactDetail Update(ContactDetail detail);

        public void Delete(ContactDetail detail);
    }
}
