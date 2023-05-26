using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesManagementWebsite.API.Services.CustomerServices;
using SalesManagementWebsite.Contracts.Dtos.Customer;
using SalesManagementWebsite.Contracts.Dtos.Response;

namespace SalesManagementWebsite.API.Controllers
{
    [Authorize]
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : Controller, ICustomerSevices
    {
        private ICustomerSevices _customerSevices { get; set; }

        public CustomerController(ICustomerSevices customerSevices)
        {
            _customerSevices = customerSevices;
        }

        [HttpGet("get-all-customers")]
        public ValueTask<ResponseHandle<CustomerOuputDto>> GetAllCustomer()
        {
            return _customerSevices.GetAllCustomer();
        }

        [HttpGet("get-customer/{id}")]
        public ValueTask<ResponseHandle<CustomerOuputDto>> GetCustomer(Guid id)
        {
            return _customerSevices.GetCustomer(id);
        }

        [HttpPost("create-customer")]
        public ValueTask<ResponseHandle<CustomerOuputDto>> CreateCustomer(CustomerCreateDto customerCreateDto)
        {
            return _customerSevices.CreateCustomer(customerCreateDto);
        }

        [HttpPut("update-customer")]
        public ValueTask<ResponseHandle<CustomerOuputDto>> UpdateCustomer(CustomerInputDto customerInputDto)
        {
            return _customerSevices.UpdateCustomer(customerInputDto);
        }

        [HttpDelete("delete-customer")]
        public ValueTask<ResponseHandle<CustomerOuputDto>> DeleteCustomer(Guid id)
        {
            return _customerSevices.DeleteCustomer(id);
        }
    }
}
