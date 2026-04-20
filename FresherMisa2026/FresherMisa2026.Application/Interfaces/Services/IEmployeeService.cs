using FresherMisa2026.Entities;
using FresherMisa2026.Entities.Employee;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FresherMisa2026.Application.Interfaces.Services
{
    public interface IEmployeeService : IBaseService<Employee>
    {
        Task<Employee> GetEmployeeByCodeAsync(string code);
        Task<IEnumerable<Employee>> GetEmployeesByDepartmentIdAsync(Guid departmentId);
        Task<IEnumerable<Employee>> GetEmployeesByPositionIdAsync(Guid positionId);

        Task<PagingResponse<Employee>> FilterEmployeesAsync(Guid? departmentId, Guid? positionId, string? gender, decimal? salaryFrom, decimal? salaryTo, DateTime? hireDateFrom, DateTime? hireDateTo, int pageSize, int pageIndex);

        Task<IEnumerable<Employee>> GetEmployeesByDeparmentCodeAsync(string deparmentCode);

        Task<int> CountEmployeesByDepartmentCodeAsync(string deparmentCode);
    }
}