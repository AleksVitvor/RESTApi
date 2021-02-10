namespace DAL.UoW
{
    using System;
    using DAL.Repository;

    public interface IUnitOfWork : IDisposable
    {
        IRepository<TSource> GetRepository<TSource>() where TSource : class;
        void Save();
    }
}
