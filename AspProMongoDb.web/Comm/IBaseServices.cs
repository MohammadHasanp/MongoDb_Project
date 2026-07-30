namespace AspProMongoDb.web.Comm
{
    public interface IBaseServices<TEntity> where TEntity : BaseEntity
    {
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(Guid userid);
        TEntity? GetById(Guid userId);
        List<TEntity> GetAll();
    }
}
