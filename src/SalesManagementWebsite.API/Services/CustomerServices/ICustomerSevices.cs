using SalesManagementWebsite.Contracts.Dtos.Customer;
using SalesManagementWebsite.Contracts.Dtos.Response;

namespace SalesManagementWebsite.API.Services.CustomerServices
{
    public interface ICustomerSevices
    {
        ValueTask<ResponseHandle<CustomerOuputDto>> GetAllCustomer();
        ValueTask<ResponseHandle<CustomerOuputDto>> GetCustomer(Guid id);
        ValueTask<ResponseHandle<CustomerOuputDto>> CreateCustomer(CustomerCreateDto customerCreateDto);
        ValueTask<ResponseHandle<CustomerOuputDto>> UpdateCustomer(CustomerInputDto customerInputDto);
        ValueTask<ResponseHandle<CustomerOuputDto>> DeleteCustomer(Guid id);
    }
}
