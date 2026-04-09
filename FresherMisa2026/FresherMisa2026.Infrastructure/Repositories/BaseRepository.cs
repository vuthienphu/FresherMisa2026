using Dapper;
using FresherMisa2026.Application.Interfaces;
using FresherMisa2026.Entities;
using FresherMisa2026.Entities.Extensions;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace FresherMisa2026.Infrastructure.Repositories
{
    /// <summary>
    // Base repository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// Created By: dvhai (09/04/2026)
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>, IDisposable where TEntity : BaseModel
    {
        //Properties
        string _connectionString = string.Empty;
        IConfiguration _configuration;
        protected IDbConnection _dbConnection = null;
        protected string _tableName;
        public Type _modelType = null;


        //Constructor
        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection")!;
            _dbConnection = new MySqlConnection(_connectionString);
            _modelType = typeof(TEntity);
            _tableName = _modelType.GetTableName();
        }


        /// <summary>
        /// Dispose connection
        /// </summary>
        /// Created By: dvhai (09/04/2026)
        public void Dispose()
        {
            if (_dbConnection != null && _dbConnection.State == ConnectionState.Open)
            {
                _dbConnection.Close();
                _dbConnection.Dispose();
            }
        }

        #region Method Get
        /// <summary>
        /// Lấy danh sách entity
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        /// Created By: dvhai (09/04/2026)
        public async Task<IEnumerable<BaseModel>> GetEntities()
        {
            return await GetEntitiesUsingCommandTextAsync();
        }

        /// <summary>
        /// Lấy tất cả theo command text
        /// </summary>
        /// <returns></returns>
        /// CREATED BY: DVHAI (11/07/2021)
        private async Task<IEnumerable<TEntity>> GetEntitiesUsingCommandTextAsync()
        {
            var query = new StringBuilder($"select * from {_tableName}");
            int whereCount = 0;

            if (_modelType.GetHasDeletedColumn())
            {
                whereCount++;
                query.Append($" where IsDeleted = FALSE");
            }

            var entities = await _dbConnection.QueryAsync<TEntity>(query.ToString(), commandType: CommandType.Text);

            return entities.ToList();
        }
        #endregion
    }
}
