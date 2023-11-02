using FirstAspWebApi.Data;
using FirstAspWebApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FirstAspWebApi.Repositary
{
    public class EntityRepository<T> : IRepository<T> where T : class
    {
        private MyContext _context;
        private DbSet<T> _table;

        public EntityRepository(MyContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        //modifing query firing simple way
        public IQueryable<T> GetAll()
        {
            return  _table.AsQueryable();//
           
           
        }

        //public List<T> GetAll(string[] innerTables)
        //{
        //   IQueryable<T> query = _table.AsQueryable();//
        //    foreach (var innerTable in innerTables)
        //    {
        //        query= query.Include(innerTable);//
        //    }
        //    return query.ToList();
        //}
        public IQueryable<T> GetById()
        {
           return _table.AsQueryable();
           
        }

        public void Detached(T entity)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }
      

        public int Add(T entity)
        {
            _table.Add(entity);
            return _context.SaveChanges();
        }

        public T Update(T entity)
        {
           _table.Update(entity);
            _context.SaveChanges();
            return entity;
        }

        public void Delete(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            var isActiveProperty = entity.GetType().GetProperty("IsActive");

            if (isActiveProperty != null)
            {
                isActiveProperty.SetValue(entity, false);
                _table.Update(entity);

            }
            else
            {
                _table.Remove(entity);

            }
            _context.SaveChanges();
        }







    }
}
