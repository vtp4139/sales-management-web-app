using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesManagementWebsite.Core.Services.SupplierServices;
using SalesManagementWebsite.Contracts.Dtos.Response;
using SalesManagementWebsite.Contracts.Dtos.Supplier;

namespace SalesManagementWebsite.Core.Controllers
{
    //[Authorize]
    [Route("api/suppliers")]
    [ApiController]
    public class SupplierController : Controller, ISupplierServices
    {
        private ISupplierServices _supplierServices { get; set; }

        public SupplierController(ISupplierServices supplierServices)
        {
            _supplierServices = supplierServices;
        }

        [HttpGet]
        public async ValueTask<ResponseHandle<SupplierOutputDto>> GetAllSuppliers()
        {
            return await _supplierServices.GetAllSuppliers();
        }

        [HttpGet("{id}")]
        public async ValueTask<ResponseHandle<SupplierOutputDto>> GetSupplier(Guid id)
        {
            return await _supplierServices.GetSupplier(id);
        }

        [HttpPost]
        public async ValueTask<ResponseHandle<SupplierOutputDto>> CreateSupplier(SupplierCreateDto supplierCreateDto)
        {
            return await _supplierServices.CreateSupplier(supplierCreateDto);
        }

        [HttpPut("{id}")]
        public async ValueTask<ResponseHandle<SupplierOutputDto>> UpdateSupplier(Guid id, SupplierUpdateDto supplierUpdateDto)
        {
            return await _supplierServices.UpdateSupplier(id, supplierUpdateDto);
        }

        [HttpDelete("{id}")]
        public async ValueTask<ResponseHandle<SupplierOutputDto>> DeleteSupplier(Guid id)
        {
            return await _supplierServices.DeleteSupplier(id);
        }
    }
}
