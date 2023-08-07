using AutoMapper;
using SalesManagementWebsite.Contracts.Dtos.Supplier;
using SalesManagementWebsite.Domain.Entities;

namespace SalesManagementWebsite.API.AutoMapper
{
    public class SupplierProfile : Profile
    {
        public SupplierProfile()
        {
            CreateMap<Supplier, SupplierOutputDto>();

            CreateMap<SupplierCreateDto, Supplier>();
        }
    }
}
