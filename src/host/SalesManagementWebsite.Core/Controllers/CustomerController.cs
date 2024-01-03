using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesManagementWebsite.Core.Services.CustomerServices;
using SalesManagementWebsite.Contracts.Dtos.Customer;
using SalesManagementWebsite.Contracts.Dtos.Response;

namespace SalesManagementWebsite.Core.Controllers
{
    [Authorize]
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : Controller, ICustomerSevices
    {
        private ICustomerSevices _customerSevices { get; set; }

        public CustomerController(ICustomerSevices customerSevices)
        {
            _customerSevices = customerSevices;
        }

        [HttpGet]
        public ValueTask<ResponseHandle<CustomerOuputDto>> GetAllCustomer()
        {
            return _customerSevices.GetAllCustomer();
        }

        [HttpGet("{id}")]
        public ValueTask<ResponseHandle<CustomerOuputDto>> GetCustomer(Guid id)
        {
            return _customerSevices.GetCustomer(id);
        }

        [HttpPost]
        public ValueTask<ResponseHandle<CustomerOuputDto>> CreateCustomer(CustomerCreateDto customerCreateDto)
        {
            return _customerSevices.CreateCustomer(customerCreateDto);
        }

        [HttpPut("{id}")]
        public ValueTask<ResponseHandle<CustomerOuputDto>> UpdateCustomer(Guid id, CustomerInputDto customerInputDto)
        {
            return _customerSevices.UpdateCustomer(id, customerInputDto);
        }

        [HttpDelete("{id}")]
        public ValueTask<ResponseHandle<CustomerOuputDto>> DeleteCustomer(Guid id)
        {
            return _customerSevices.DeleteCustomer(id);
        }
    }
}
