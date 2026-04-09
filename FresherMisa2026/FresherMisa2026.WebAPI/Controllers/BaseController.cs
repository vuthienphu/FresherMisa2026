using FresherMisa2026.Application.Interfaces;
using FresherMisa2026.Application.Interfaces.Services;
using FresherMisa2026.Entities;
using FresherMisa2026.Entities.Department;
using Microsoft.AspNetCore.Mvc;
using static Dapper.SqlMapper;

namespace FresherMisa2026.WebAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class BaseController<TEntity> : ControllerBase
    {

        private readonly IBaseService<TEntity> _baseService;

        public BaseController(IBaseService<TEntity> baseService)
        {
            _baseService = baseService;
        }


        [HttpGet()]
        public async Task<ServiceResponse> Get()
        {
            var response = new ServiceResponse();
            response.Data = await _baseService.GetEntities();
            response.IsSuccess = true;
            return response;
        }
    }
}
