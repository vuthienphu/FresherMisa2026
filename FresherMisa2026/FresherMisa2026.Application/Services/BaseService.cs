using FresherMisa2026.Application.Interfaces;
using FresherMisa2026.Application.Interfaces.Services;
using FresherMisa2026.Entities;
using FresherMisa2026.Entities.Enums;
using FresherMisa2026.Entities.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace FresherMisa2026.Application.Services
{
    /// <summary>
    /// Service dùng chung
    /// </summary>
    /// <typeparam name="TEntity">Loại thực thể</typeparam>
    /// CREATED BY: DVHAI (11/07/2021)
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseModel
    {
        #region Declare
        IBaseRepository<TEntity> _baseRepository;
        protected ServiceResponse _serviceResult = null;
        public Type _modelType = null;
        protected string _tableName = string.Empty;
        #endregion

        #region Constructer
        public BaseService(IBaseRepository<TEntity> baseRepository)
        {
            _baseRepository = baseRepository;
            _modelType = typeof(TEntity);
            _tableName = _modelType.GetTableName().ToLowerInvariant();
            _serviceResult = new ServiceResponse()
            {
                IsSuccess = true,
                Code = (int)ResponseCode.Success,
            };
        }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// CREATED BY: DVHAI 11/07/2021
        public async Task<IEnumerable<TEntity>> GetEntities()
        {
            var entities = await _baseRepository.GetEntities();
            return entities.Cast<TEntity>();
        }
        #endregion
    }
}
