using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.Infrastructure
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(Guid id);
        void Add(TEntity entity);
   
        void Update(TEntity entity);

        TEntity Create();
    }
}