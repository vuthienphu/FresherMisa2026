using FresherMisa2026.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FresherMisa2026.Application.Interfaces
{
    public interface IBaseRepository<TEntity>
    {
        public Task<IEnumerable<BaseModel>> GetEntities();
    }
}
