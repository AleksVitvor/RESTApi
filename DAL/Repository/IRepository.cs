namespace DAL.Repository
{
    using System;
    using System.Collections.Generic;

    public interface IRepository<TSource> : IDisposable where TSource : class
    { 
        IEnumerable<TSource> GetItemsList();

        IEnumerable<TSource> GetItemsList(Func<TSource, bool> predicate);

        TSource GetItem(Func<TSource, bool> predicate);

        void Create(TSource item);

        void Delete(TSource item);

        void Delete(Func<TSource, bool> predicate);

        void Update(TSource item);
    }
}
