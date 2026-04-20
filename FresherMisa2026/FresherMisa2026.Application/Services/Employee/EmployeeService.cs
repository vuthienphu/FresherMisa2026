using FresherMisa2026.Application.Interfaces;
using FresherMisa2026.Application.Interfaces.Repositories;
using FresherMisa2026.Application.Interfaces.Services;
using FresherMisa2026.Entities;
using FresherMisa2026.Entities.Employee;
using FresherMisa2026.Entities.Position;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FresherMisa2026.Application.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(
            IBaseRepository<Employee> baseRepository,
            IEmployeeRepository employeeRepository
            ) : base(baseRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Employee> GetEmployeeByCodeAsync(string code)
        {
            var employee = await _employeeRepository.GetEmployeeByCode(code);
            if (employee == null)
                throw new Exception("Employee not found");

            return employee;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByDepartmentIdAsync(Guid departmentId)
        {
            return await _employeeRepository.GetEmployeesByDepartmentId(departmentId);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByPositionIdAsync(Guid positionId)
        {
            return await _employeeRepository.GetEmployeesByPositionId(positionId);
        }

        public async Task<PagingResponse<Employee>> FilterEmployeesAsync(Guid? departmentId, Guid? positionId, string? gender, decimal? salaryFrom, decimal? salaryTo, DateTime? hireDateFrom, DateTime? hireDateTo, int pageSize, int pageIndex)
        {
            return await _employeeRepository.FilterEmployees(departmentId, positionId, gender, salaryFrom, salaryTo, hireDateFrom, hireDateTo, pageSize, pageIndex);
        }

        protected override List<ValidationError> ValidateCustom(Employee employee)
        {
            var errors = new List<ValidationError>();

            if (!string.IsNullOrEmpty(employee.EmployeeCode) && employee.EmployeeCode.Length > 20)
            {
                errors.Add(new ValidationError("EmployeeCode", "Mã nhân viên không được vượt quá 20 ký tự"));
            }

            if (string.IsNullOrEmpty(employee.EmployeeName))
            {
                errors.Add(new ValidationError("EmployeeName", "Tên nhân viên không được để trống"));
            }

            //1. Check mã nhân viên không được trùng

            if (!string.IsNullOrEmpty(employee.EmployeeCode))
            {
                var existingEmployeeCode = _employeeRepository.GetEmployeeByCode(employee.EmployeeCode).GetAwaiter().GetResult();
                if (existingEmployeeCode != null && existingEmployeeCode.EmployeeID != employee.EmployeeID)
                {
                    errors.Add(new ValidationError("EmployeeCode", "Mã nhân viên đã tồn tại"));
                }
            }

            // 2. Email format
            if (!string.IsNullOrEmpty(employee.Email))
            {
                var isValidEmail = Regex.IsMatch(employee.Email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

                if (!isValidEmail)
                {
                    errors.Add(new ValidationError("Email", "Email không đúng định dạng"));
                }
            }

            if (!string.IsNullOrEmpty(employee.PhoneNumber))
            {
                var isValidPhone = Regex.IsMatch(employee.PhoneNumber,
                    @"^(0|\+84)[0-9]{9}$");

                if (!isValidPhone)
                {
                    errors.Add(new ValidationError("PhoneNumber", "Số điện thoại không đúng định dạng"));
                }
            }

            // 4. Ngày sinh
            if (employee.DateOfBirth.HasValue &&
                employee.DateOfBirth >= DateTime.Now)
            {
                errors.Add(new ValidationError("DateOfBirth", "Ngày sinh phải nhỏ hơn ngày hiện tại"));
            }



            return errors;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByDeparmentCodeAsync(string deparmentCode)
        {
            return await _employeeRepository.GetEmployeesByDeparmentCode(deparmentCode);
        }

        public async Task<int> CountEmployeesByDepartmentCodeAsync(string deparmentCode)
        {
            return await _employeeRepository.CountEmployeesByDepartmentCode(deparmentCode);
        }
    }
}