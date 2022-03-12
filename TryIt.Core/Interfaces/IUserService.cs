using TryIt.Core.DTOs;
using TryIt.Core.Entities;

namespace TryIt.Core.Interfaces
{
    public interface IUserService
    {
        Task<AuthenticateResponseDTO> Authenticate(AuthenticateRequestDTO model);
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(Guid id);
        Task Register(RegisterRequestDTO model);
        Task Update(Guid id, UpdateRequestDTO model);
        Task Delete(Guid id);
    }
}
