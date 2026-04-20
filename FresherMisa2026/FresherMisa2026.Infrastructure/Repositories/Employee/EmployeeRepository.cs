using Dapper;
using FresherMisa2026.Application.Extensions;
using FresherMisa2026.Application.Interfaces.Repositories;
using FresherMisa2026.Entities.Employee;
using FresherMisa2026.Entities.Position;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;
using System;
using Dapper;

namespace FresherMisa2026.Infrastructure.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IConfiguration configuration, IMemoryCache memoryCache = null) : base(configuration, memoryCache)
        {
        }

        public async Task<Employee> GetEmployeeByCode(string code)
        {
            string query = SQLExtension.GetQuery("Employee.GetByCode");
            var param = new Dictionary<string, object>
            {
                {"@EmployeeCode", code }
            };
            using var connection = await OpenConnectionAsync();
            return await connection.QueryFirstOrDefaultAsync<Employee>(query, param, commandType: System.Data.CommandType.Text);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByDepartmentId(Guid departmentId)
        {
            string query = SQLExtension.GetQuery("Employee.GetByDepartmentId");
            var param = new Dictionary<string, object>
            {
                {"@DepartmentID", departmentId }
            };
            using var connection = await OpenConnectionAsync();
            return await connection.QueryAsync<Employee>(query, param, commandType: System.Data.CommandType.Text);
        }


        public async Task<IEnumerable<Employee>> GetEmployeesByPositionId(Guid positionId)
        {
            string query = SQLExtension.GetQuery("Employee.GetByPositionId");
            var param = new Dictionary<string, object>
            {
                {"@PositionID", positionId }
            };
            using var connection = await OpenConnectionAsync();
            return await connection.QueryAsync<Employee>(query, param, commandType: System.Data.CommandType.Text);
        }

        public async Task<IEnumerable<Employee>> FilterEmployees(Guid? departmentId, Guid? positionId, string? gender, decimal? salaryFrom, decimal? salaryTo,DateTime? hireDateFrom,DateTime? hireDateTo)
        {
            string query = SQLExtension.GetQuery("Employee.Filter");

            var param = new Dictionary<string, object>
    {
        {"@DepartmentID", departmentId},
        {"@PositionID", positionId},
        {"@Gender", gender},
         {"@SalaryFrom", salaryFrom},
        {"@SalaryTo", salaryTo},
        {"@HireDateFrom", hireDateFrom},
        {"@HireDateTo", hireDateTo}
    };

            using var connection = await OpenConnectionAsync();
            return await connection.QueryAsync<Employee>(query, param, commandType: System.Data.CommandType.Text);
        }

      
        public async Task<IEnumerable<Employee>> GetEmployeesByDeparmentCode(string deparmentCode)
        {
            string query = SQLExtension.GetQuery("Employee.DeparmentCode");
            var param = new Dictionary<string, object>
            {
                {"@DeparmentCode", deparmentCode}
            };
            using var connection = await OpenConnectionAsync();
            return await connection.QueryAsync<Employee>(query, param, commandType: System.Data.CommandType.Text);
        }

        public async Task<int> CountEmployeesByDepartmentCode(string deparmentCode)
        {
            string query = SQLExtension.GetQuery("CountEmployee.DeparmentCode");

            var param = new Dictionary<string, object>
    {
        {"@DeparmentCode", deparmentCode}
    };

            using var connection = await OpenConnectionAsync();
            return await connection.ExecuteScalarAsync<int>(query, param, commandType: System.Data.CommandType.Text);
        }
    }
}