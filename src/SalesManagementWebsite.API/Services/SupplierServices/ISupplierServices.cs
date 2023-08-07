using SalesManagementWebsite.Contracts.Dtos.Response;
using SalesManagementWebsite.Contracts.Dtos.Supplier;

namespace SalesManagementWebsite.API.Services.SupplierServices
{
    public interface ISupplierServices
    {
        ValueTask<ResponseHandle<SupplierOutputDto>> GetAllSuppliers();
        ValueTask<ResponseHandle<SupplierOutputDto>> GetSupplier(Guid id);
        ValueTask<ResponseHandle<SupplierOutputDto>> CreateSupplier(SupplierCreateDto supplierCreateDto);
        ValueTask<ResponseHandle<SupplierOutputDto>> UpdateSupplier(SupplierUpdateDto supplierUpdateDto);
        ValueTask<ResponseHandle<SupplierOutputDto>> DeleteSupplier(Guid id);
    }
}
