using SalesManagementWebsite.Contracts.Dtos.Response;
using SalesManagementWebsite.Contracts.Dtos.Role;

namespace SalesManagementWebsite.Core.Services.RoleServices
{
    public interface IRoleServices
    {
        ValueTask<ResponseHandle<RoleOutputDto>> GetAllRoles();
        ValueTask<ResponseHandle<RoleOutputDto>> GetRole(Guid id);
        ValueTask<ResponseHandle<RoleOutputDto>> CreateRole(RoleInputDto roleInputDto);
        ValueTask<ResponseHandle<RoleOutputDto>> UpdateRole(RoleUpdateDto roleUpdateDto);
        ValueTask<ResponseHandle<RoleOutputDto>> DeleteRole(Guid id);
    }
}
