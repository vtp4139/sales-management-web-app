using SalesManagementWebsite.Contracts.Dtos.User;
using SalesManagementWebsite.Domain.UnitOfWork;

namespace SalesManagementWebsite.API.Services.User
{
    public class UserService : IUserService
    {
        public IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public Task Register(UserRegisterDto userRegisterDto)
        {
            throw new NotImplementedException();
        }
    }
}
