using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTasksClassLib.DataAccess.Repository.IRepository
{
    internal interface IGRepository<TEntity> where TEntity : class
    {
        Task CreateAsync(TEntity entity);
        IQueryable<TEntity> GetAll();
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
