using FirstAspWebApi.Models;

namespace FirstAspWebApi.Repositary
{
    public class DetailService : IDetailsService
    {
        private IRepository<ContactDetail> _repository;


        public DetailService(IRepository<ContactDetail> repository)
        {
            _repository = repository;
        }

        public int Add(ContactDetail detail)
        {
           return _repository.Add(detail);
           
        }

        public void Delete(ContactDetail detail)
        {
            _repository.Delete(detail);
        }

        public List<ContactDetail> GetAll()
        {
            var user= _repository.GetAll();

            return user.ToList();
        }

        public ContactDetail GetById(int id)
        {
            var queryTable = _repository.GetById();
           var detail= queryTable.Where(det=>det.DetailId==id).FirstOrDefault();
            if (detail != null)
            {
                _repository.Detached(detail);
            }
            return detail;
        }

        public ContactDetail Update(ContactDetail detail)
        {
          return  _repository.Update(detail);
        }
    }
}
