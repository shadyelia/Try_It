using AutoMapper;
using TryIt.Core.DTOs;
using TryIt.Core.Entities;
using TryIt.Infrastructure.Data;
using TryIt.Web.Authorization;
using TryIt.SharedKernel.Helpers;

namespace TryIt.Web.Services
{
    public interface IUserService
    {
        AuthenticateResponseDTO Authenticate(AuthenticateRequestDTO model);
        IEnumerable<User> GetAll();
        User GetById(Guid id);
        void Register(RegisterRequestDTO model);
        void Update(Guid id, UpdateRequestDTO model);
        void Delete(Guid id);
    }
    public class UserService : IUserService
    {
        private SqliteDataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public UserService(SqliteDataContext context, IJwtUtils jwtUtils, IMapper mapper)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }

        public AuthenticateResponseDTO Authenticate(AuthenticateRequestDTO model)
        {
            var user = _context.Users.SingleOrDefault(x => x.Username == model.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password,user.PasswordHash))
                throw new AppException("Username or password is incorrect");

            var response = _mapper.Map<AuthenticateResponseDTO>(user);
            response.Token = _jwtUtils.GenerateToken(user);
            return response;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetById(Guid id)
        {
            return getUser(id);
        }

        public void Register(RegisterRequestDTO model)
        {
            // validate
            if (_context.Users.Any(x => x.Username == model.Username))
                throw new AppException("Username '" + model.Username + "' is already taken");

            // map model to new user object
            var user = _mapper.Map<User>(model);

            // hash password
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            // save user
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Update(Guid id, UpdateRequestDTO model)
        {
            var user = getUser(id);

            // validate
            if (model.Username != user.Username && _context.Users.Any(x => x.Username == model.Username))
                throw new AppException("Username '" + model.Username + "' is already taken");

            // hash password if it was entered
            if (!string.IsNullOrEmpty(model.Password))
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            // copy model to user and save
            _mapper.Map(model, user);
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var user = getUser(id);
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
        private User getUser(Guid id)
        {
            var user = _context.Users.Find(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }
    }
}
