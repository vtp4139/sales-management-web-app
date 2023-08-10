using AutoMapper;
using SalesManagementWebsite.Contracts.Dtos.Customer;
using SalesManagementWebsite.Contracts.Dtos.Item;
using SalesManagementWebsite.Contracts.Dtos.Response;
using SalesManagementWebsite.Domain.Entities;
using SalesManagementWebsite.Domain.UnitOfWork;
using System.Net;
using System.Text.Json;

namespace SalesManagementWebsite.Core.Services.CustomerServices
{
    public class CustomerSevices : ICustomerSevices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public CustomerSevices(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CustomerSevices> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async ValueTask<ResponseHandle<CustomerOuputDto>> GetAllCustomer()
        {
            try
            {
                var gCustomerList = await _unitOfWork.CustomerRepository.GetAllAsync();

                if (gCustomerList == null)
                {
                    return new ResponseHandle<CustomerOuputDto>
                    {
                        IsSuccess = false,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Data = null,
                        ErrorMessage = $"Can not get list of [Customer]"
                    };
                }

                var cusListOutput = _mapper.Map<List<CustomerOuputDto>>(gCustomerList);

                return new ResponseHandle<CustomerOuputDto>
                {
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = null,
                    ListData = cusListOutput,
                    ErrorMessage = string.Empty
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"CustomerSevices -> GetAllCustomer() " +
                                 $"- Have exception: {ex}, at {DateTime.UtcNow.ToLongTimeString()}");
                throw;
            }
        }

        public async ValueTask<ResponseHandle<CustomerOuputDto>> GetCustomer(Guid id)
        {
            try
            {
                var gCustomer = await _unitOfWork.CustomerRepository.GetAsync(cus => cus.Id.Equals(id));

                if (gCustomer == null)
                {
                    return new ResponseHandle<CustomerOuputDto>
                    {
                        IsSuccess = false,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Data = null,
                        ErrorMessage = $"Can not get [Customer] with [id]: {id}"
                    };
                }

                var itemOutput = _mapper.Map<CustomerOuputDto>(gCustomer);

                return new ResponseHandle<CustomerOuputDto>
                {
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = itemOutput,
                    ErrorMessage = string.Empty
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"CustomerSevices -> GetCustomer({JsonSerializer.Serialize(id)}) " +
                                 $"- Have exception: {ex}, at {DateTime.UtcNow.ToLongTimeString()}");
                throw;
            }
        }

        public async ValueTask<ResponseHandle<CustomerOuputDto>> CreateCustomer(CustomerCreateDto customerCreateDto)
        {
            try
            {
                var customer = _mapper.Map<Customer>(customerCreateDto);

                _unitOfWork.CustomerRepository.Add(customer);
                await _unitOfWork.CommitAsync();

                var itemOutput = _mapper.Map<CustomerOuputDto>(customer);

                return new ResponseHandle<CustomerOuputDto>
                {
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = itemOutput,
                    ErrorMessage = string.Empty
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"CustomerSevices -> CreateCustomer({JsonSerializer.Serialize(customerCreateDto)}) " +
                                 $"- Have exception: {ex}, at {DateTime.UtcNow.ToLongTimeString()}");
                throw;
            }
        }

        public async ValueTask<ResponseHandle<CustomerOuputDto>> UpdateCustomer(CustomerInputDto customerInputDto)
        {
            try
            {
                var gCustomer = await _unitOfWork.CustomerRepository.GetAsync(c => c.Id.Equals(customerInputDto.Id));

                if (gCustomer == null)
                {
                    return new ResponseHandle<CustomerOuputDto>
                    {
                        IsSuccess = false,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Data = null,
                        ErrorMessage = $"Can not get the [Customer]: {JsonSerializer.Serialize(customerInputDto)}"
                    };
                }

                //Mapping field modify
                gCustomer.CustomerName = customerInputDto.CustomerName;
                gCustomer.Address = customerInputDto.Address;
                gCustomer.City = customerInputDto.City;
                gCustomer.PostalCode = customerInputDto.PostalCode;
                gCustomer.Phone = customerInputDto.Phone;
                gCustomer.Fax = customerInputDto.Fax;
                gCustomer.ModifiedDate = DateTime.Now;

                _unitOfWork.CustomerRepository.Update(gCustomer);
                await _unitOfWork.CommitAsync();

                var itemOutput = _mapper.Map<CustomerOuputDto>(gCustomer);

                return new ResponseHandle<CustomerOuputDto>
                {
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = itemOutput,
                    ErrorMessage = string.Empty
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"CustomerSevices -> UpdateCustomer({JsonSerializer.Serialize(customerInputDto)}) " +
                                 $"- Have exception: {ex}, at {DateTime.UtcNow.ToLongTimeString()}");
                throw;
            }
        }

        public async ValueTask<ResponseHandle<CustomerOuputDto>> DeleteCustomer(Guid id)
        {
            try
            {
                var gCustomer = await _unitOfWork.CustomerRepository.GetAsync(c => c.Id.Equals(id));

                if (gCustomer == null)
                {
                    return new ResponseHandle<CustomerOuputDto>
                    {
                        IsSuccess = false,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Data = null,
                        ErrorMessage = $"Can not get [Item] with [id]: {id}"
                    };
                }

                _unitOfWork.CustomerRepository.Remove(gCustomer);
                await _unitOfWork.CommitAsync();


                var cateOutput = _mapper.Map<CustomerOuputDto>(gCustomer);

                return new ResponseHandle<CustomerOuputDto>
                {
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = cateOutput,
                    ErrorMessage = string.Empty
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"CustomerSevices -> DeleteCustomer({JsonSerializer.Serialize(id)}) " +
                                 $"- Have exception: {ex}, at {DateTime.UtcNow.ToLongTimeString()}");
                throw;
            }
        }
    }
}
