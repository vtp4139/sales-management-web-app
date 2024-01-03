using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesManagementWebsite.Contracts.Dtos.Response;
using SalesManagementWebsite.Contracts.Dtos.Role;
using SalesManagementWebsite.Core.Services.RoleServices;

namespace SalesManagementWebsite.Core.Controllers
{
    [Authorize(Roles = "administration")]
    [Route("api/roles")]
    [ApiController]
    public class RoleController : Controller, IRoleServices
    {
        private IRoleServices _roleServices { get; set; }

        public RoleController(IRoleServices roleServices)
        {
            _roleServices = roleServices;
        }

        [HttpGet]
        public async ValueTask<ResponseHandle<RoleOutputDto>> GetAllRoles()
        {
            return await _roleServices.GetAllRoles();
        }

        [HttpGet("{id}")]
        public async ValueTask<ResponseHandle<RoleOutputDto>> GetRole(Guid id)
        {
            return await _roleServices.GetRole(id);
        }

        [HttpPost]
        public async ValueTask<ResponseHandle<RoleOutputDto>> CreateRole(RoleInputDto roleInputDto)
        {
            return await _roleServices.CreateRole(roleInputDto);
        }

        [HttpPut("{id}")]
        public async ValueTask<ResponseHandle<RoleOutputDto>> UpdateRole(Guid id, RoleUpdateDto roleUpdateDto)
        {
            return await _roleServices.UpdateRole(id, roleUpdateDto);
        }

        [HttpDelete("{id}")]
        public async ValueTask<ResponseHandle<RoleOutputDto>> DeleteRole(Guid id)
        {
            return await _roleServices.DeleteRole(id);
        }
    }
}
