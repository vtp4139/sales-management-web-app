﻿using SalesManagementWebsite.Contracts.Dtos.Response;
using SalesManagementWebsite.Contracts.Dtos.Supplier;

namespace SalesManagementWebsite.Core.Services.SupplierServices
{
    public interface ISupplierServices
    {
        ValueTask<ResponseHandle<SupplierOutputDto>> GetAllSuppliers();
        ValueTask<ResponseHandle<SupplierOutputDto>> GetSupplier(Guid id);
        ValueTask<ResponseHandle<SupplierOutputDto>> CreateSupplier(SupplierCreateDto supplierCreateDto);
        ValueTask<ResponseHandle<SupplierOutputDto>> UpdateSupplier(Guid id, SupplierUpdateDto supplierUpdateDto);
        ValueTask<ResponseHandle<SupplierOutputDto>> DeleteSupplier(Guid id);
    }
}
