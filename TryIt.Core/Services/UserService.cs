using AutoMapper;
using TryIt.Core.DTOs;
using TryIt.Core.Entities;
using TryIt.Core.Interfaces;
using TryIt.Core.Specifications.UserSpecifications;
using TryIt.SharedKernel.Authorization;
using TryIt.SharedKernel.Helpers;
using TryIt.SharedKernel.Interfaces;

namespace TryIt.Core.Services
{
    public class UserService : IUserService
    {
        private IRepository<User> _repo;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public UserService(IRepository<User> repo, IJwtUtils jwtUtils, IMapper mapper)
        {
            _repo = repo;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }

        public async Task<AuthenticateResponseDTO> Authenticate(AuthenticateRequestDTO model)
        {
            var user = await getUser(model.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
                throw new AppException("Username or password is incorrect");

            var response = _mapper.Map<AuthenticateResponseDTO>(user);
            response.Token = _jwtUtils.GenerateToken(user.Id.ToString());
            return response;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var userSpec = new GetAllUsers(8,0);
            var users = await _repo.ListAsync(userSpec);
            return users;
        }

        public async Task<User> GetById(Guid id)
        {
            return await getUser(id);
        }

        public async Task Register(RegisterRequestDTO model)
        {
            // validate
            User oldUser = await getUser(model.Username);
            if (oldUser != null)
                throw new AppException("Username '" + model.Username + "' is already taken");

            // map model to new user object
            var user = _mapper.Map<User>(model);

            // hash password
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            // save user
            await _repo.AddAsync(user);
        }

        public async Task Update(Guid id, UpdateRequestDTO model)
        {
            var user = await getUser(id);
            var userByUsername = await getUser(model.Username);

            // validate
            if (model.Username != user.Username && userByUsername != null)
                throw new AppException("Username '" + model.Username + "' is already taken");

            // hash password if it was entered
            if (!string.IsNullOrEmpty(model.Password))
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            // copy model to user and save
            _mapper.Map(model, user);
            await _repo.UpdateAsync(user);
        }

        public async Task Delete(Guid id)
        {
            var user = await getUser(id);
            await _repo.DeleteAsync(user);
        }
        private async Task<User> getUser(string username)
        {
            var userSpec = new GetAUserByUserNameSpec(username);
            var user = await _repo.GetBySpecAsync(userSpec);
            return user;
        }
        private async Task<User> getUser(Guid id)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }
    }
}
