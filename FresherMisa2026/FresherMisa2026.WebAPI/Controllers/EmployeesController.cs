using FresherMisa2026.Application.Interfaces.Services;
using FresherMisa2026.Entities;
using FresherMisa2026.Entities.Employee;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FresherMisa2026.WebAPI.Controllers
{
    [ApiController]
    public class EmployeesController : BaseController<Employee>
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(
            IEmployeeService employeeService) : base(employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("Code/{code}")]
        public async Task<ActionResult<ServiceResponse>> GetByCode(string code)
        {
            var response = new ServiceResponse();
            response.Data = await _employeeService.GetEmployeeByCodeAsync(code);
            response.IsSuccess = true;

            return response;
        }

        [HttpGet("Department/id/{departmentId}")]
        public async Task<ActionResult<ServiceResponse>> GetByDepartmentId(Guid departmentId)
        {
            var response = new ServiceResponse();
            response.Data = await _employeeService.GetEmployeesByDepartmentIdAsync(departmentId);
            response.IsSuccess = true;

            return response;
        }

        [HttpGet("Position/{positionId}")]
        public async Task<ActionResult<ServiceResponse>> GetByPositionId(Guid positionId)
        {
            var response = new ServiceResponse();
            response.Data = await _employeeService.GetEmployeesByPositionIdAsync(positionId);
            response.IsSuccess = true;

            return response;
        }

        [HttpGet("filter")]
        public async Task<ActionResult<ServiceResponse>> Filter(
            Guid? departmentId,
            Guid? positionId,
            string? gender,
            decimal? salaryFrom,
            decimal? salaryTo,
            DateTime? hireDateFrom,
            DateTime? hireDateTo,
            int pageSize = 10,
            int pageIndex = 1
            )
        {
            var response = new ServiceResponse();

            response.Data = await _employeeService
                .FilterEmployeesAsync(departmentId, positionId, gender, salaryFrom, salaryTo, hireDateFrom, hireDateTo, pageSize, pageIndex);

            response.IsSuccess = true;

            return response;
        }

        [HttpGet("Department/code/{departmentCode}")]
        public async Task<ActionResult<ServiceResponse>> GetByDepartmentCode(string departmentCode)
        {
            var response = new ServiceResponse();
            response.Data = await _employeeService.GetEmployeesByDeparmentCodeAsync(departmentCode);
            response.IsSuccess = true;

            return response;
        }

        [HttpGet("Department/{departmentCode}/count-employee")]
        public async Task<ActionResult<ServiceResponse>> CountByDepartmentCode(string departmentCode)
        {
            var response = new ServiceResponse();

            response.Data = await _employeeService
                .CountEmployeesByDepartmentCodeAsync(departmentCode);

            response.IsSuccess = true;

            return response;
        }
    }
}