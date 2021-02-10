namespace DAL.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;

    public class Repository<TSource> : IRepository<TSource> where TSource : class
    {
        private readonly DbContext context;
        private readonly DbSet<TSource> set;
        public Repository(DbContext context)
        {
            this.context = context;
            set = this.context.Set<TSource>();
        }

        public void Create(TSource item)
        {
            set.Add(item);
        }

        public void Delete(TSource item)
        {
            set.Remove(item);
        }

        public void Delete(Func<TSource, bool> predicate)
        {
            set.RemoveRange(set.Where(predicate));
        }

        public TSource GetItem(Func<TSource, bool> predicate)
        {
            return set.Where(predicate).SingleOrDefault();
        }

        public IEnumerable<TSource> GetItemsList()
        {
            return set.AsEnumerable();
        }

        public IEnumerable<TSource> GetItemsList(Func<TSource, bool> predicate)
        {
            return set.Where(predicate).AsEnumerable();
        }

        public void Update(TSource item)
        {
            context.Entry(item).State = EntityState.Modified;
        }

        #region IDisposableImpl
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
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
    }
}
