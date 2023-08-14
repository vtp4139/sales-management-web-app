using AutoMapper;
using SalesManagementWebsite.Contracts.Dtos.Customer;
using SalesManagementWebsite.Domain.Entities;

namespace SalesManagementWebsite.Core.AutoMapper
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerInputDto, Customer>();
            CreateMap<CustomerCreateDto, Customer>();
            CreateMap<Customer, CustomerOuputDto>();
        }
    }
}
