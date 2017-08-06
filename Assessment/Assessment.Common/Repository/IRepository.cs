using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Model
{
    public interface IRepository<TEntity> : IEnumerable<TEntity>
        where TEntity : class, IEntity
    {
        TEntity Get(int id);
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        bool Delete(int id);
    }
}
