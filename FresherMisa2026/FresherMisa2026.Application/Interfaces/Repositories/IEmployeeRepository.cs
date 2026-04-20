using FresherMisa2026.Entities;
using FresherMisa2026.Entities.Employee;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FresherMisa2026.Application.Interfaces.Repositories
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        Task<Employee> GetEmployeeByCode(string code);
        Task<IEnumerable<Employee>> GetEmployeesByDepartmentId(Guid departmentId);
        Task<IEnumerable<Employee>> GetEmployeesByPositionId(Guid positionId);

        Task<PagingResponse<Employee>> FilterEmployees(Guid? departmentId, Guid? positionId, string? gender, decimal? salaryFrom, decimal? salaryTo, DateTime? hireDateFrom, DateTime? hireDateTo, int pageSize, int pageIndex);

        Task<IEnumerable<Employee>> GetEmployeesByDeparmentCode(string deparmentCode);

        Task<int> CountEmployeesByDepartmentCode(string deparmentCode);
    }
}
