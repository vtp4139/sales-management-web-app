using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesManagementWebsite.API.Services.SupplierServices;
using SalesManagementWebsite.Contracts.Dtos.Response;
using SalesManagementWebsite.Contracts.Dtos.Supplier;

namespace SalesManagementWebsite.API.Controllers
{
    //[Authorize]
    [Route("api/supplier")]
    [ApiController]
    public class SupplierController : Controller, ISupplierServices
    {
        private ISupplierServices _supplierServices { get; set; }

        public SupplierController(ISupplierServices supplierServices)
        {
            _supplierServices = supplierServices;
        }

        [HttpGet("get-all-supplier")]
        public async ValueTask<ResponseHandle<SupplierOutputDto>> GetAllSuppliers()
        {
            return await _supplierServices.GetAllSuppliers();
        }

        [HttpGet("get-supplier/{id}")]
        public async ValueTask<ResponseHandle<SupplierOutputDto>> GetSupplier(Guid id)
        {
            return await _supplierServices.GetSupplier(id);
        }

        [HttpPost("create-supplier")]
        public async ValueTask<ResponseHandle<SupplierOutputDto>> CreateSupplier(SupplierCreateDto supplierCreateDto)
        {
            return await _supplierServices.CreateSupplier(supplierCreateDto);
        }

        [HttpPut("update-supplier")]
        public async ValueTask<ResponseHandle<SupplierOutputDto>> UpdateSupplier(SupplierUpdateDto supplierUpdateDto)
        {
            return await _supplierServices.UpdateSupplier(supplierUpdateDto);
        }

        [HttpDelete("delete-supplier")]
        public async ValueTask<ResponseHandle<SupplierOutputDto>> DeleteSupplier(Guid id)
        {
            return await _supplierServices.DeleteSupplier(id);
        }
    }
}
