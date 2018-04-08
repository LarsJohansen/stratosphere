using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Persistence.Abstract;

namespace Persistence
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;
        private readonly DbSet<TEntity> _entities;
        public Repository(DbContext context)
        {
            Context = context;
            _entities = context.Set<TEntity>();

        }

        public TEntity Get(int id)
        {
            return _entities.Find(id);
        }

        public TEntity Get(long id)
        {
            return _entities.Find(id);
        }


        public IEnumerable<TEntity> GetAll()
        {
            return _entities.ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Where(predicate);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.SingleOrDefault(predicate);
        }
        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.FirstOrDefault(predicate);
        }

        public void Add(TEntity entity)
        {
            _entities.Add(entity);
        }
        public void AddRange(IEnumerable<TEntity> entities)
        {
            _entities.AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            _entities.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _entities.RemoveRange(entities);
        }
    }
}
