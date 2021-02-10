namespace DAL.UoW
{
    using System;
    using System.Collections.Generic;
    using DAL.Repository;
    using Microsoft.EntityFrameworkCore;

    public class UnitOfWork : IUnitOfWork
    {
        private Dictionary<string, object> _repositories;
        private readonly DbContext _db;

        public UnitOfWork(DbContext dbContext)
        {
            _db = dbContext;
        }


        #region IDisposableImpl
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        public IRepository<TSource> GetRepository<TSource>() where TSource : class
        {
            _repositories ??= new Dictionary<string, object>();
            var type = typeof(TSource).Name;

            if (_repositories.ContainsKey(type)) return (IRepository<TSource>)_repositories[type];
            var repositoryType = typeof(Repository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TSource)), _db);
            _repositories.Add(type, repositoryInstance);
            return (IRepository<TSource>)_repositories[type];
        }

        public void Save()
        {
            using var transaction = _db.Database.BeginTransaction();
            _db.SaveChanges();
            transaction.Commit();
        }
    }
}
