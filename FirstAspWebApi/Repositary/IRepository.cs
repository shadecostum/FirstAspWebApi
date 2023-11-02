namespace FirstAspWebApi.Repositary
{
    public interface IRepository<T>
    {
        // public List<T> GetAll(string[] innerTables);//inner mentioning old 

        public IQueryable<T> GetAll();//modified for query passing

        public IQueryable<T> GetById();//modified
                                       // public T GetById(int id);//old

        public void Detached(T entity);

        public int Add(T entity);

        public T Update(T entity);

        public void Delete(T entity);


    }
}
