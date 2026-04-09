using System;
using System.Collections.Generic;
using System.Text;

namespace FresherMisa2026.Application.Interfaces.Services
{
    public interface IBaseService<TEntity>
    {
        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// CREATED BY: DVHAI 11/07/2021
        Task<IEnumerable<TEntity>> GetEntities();
    }
}
