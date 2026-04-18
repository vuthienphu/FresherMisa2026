using FresherMisa2026.Entities.Employee;
using System;
using System.Collections.Generic;

namespace FresherMisa2026.Application.Interfaces.Repositories
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        Task<Employee> GetEmployeeByCode(string code);
        Task<IEnumerable<Employee>> GetEmployeesByDepartmentId(Guid departmentId);
        Task<IEnumerable<Employee>> GetEmployeesByPositionId(Guid positionId);
        Task<IEnumerable<Employee>> FilterEmployees(Guid? departmentId, Guid? positionId, string? gender, decimal? salaryFrom, decimal? salaryTo, DateTime? hireDateFrom, DateTime? hireDateTo);
    }
}
