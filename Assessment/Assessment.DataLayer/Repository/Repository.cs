using Assessment.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.DataLayer
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        private List<TEntity> _collection = new List<TEntity>();
        public Repository(List<TEntity> source = null)
        {
            _collection = source ?? new List<TEntity>();
        }
        public TEntity Add(TEntity entity)
        {
            _collection.Add(entity);
            return entity;
        }

        public bool Delete(int id)
        {
            var entity = _collection.ElementAtOrDefault(id);
            if (entity != default(TEntity))
            {
                _collection.Remove(entity);
            }
            return entity != default(TEntity);
        }

        public TEntity Get(int id)
        {
            return _collection.ElementAtOrDefault(id);
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        public TEntity Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        public void Dispose()
        {
            if (_collection?.Any() ?? false)
            {
                _collection.Clear();
            }
        }
    }
}
