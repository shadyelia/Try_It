namespace TryIt.SharedKernel.Authorization
{
    public interface IJwtUtils
    {
        public string GenerateToken(string userId);
        public Guid? ValidateToken(string? token);
    }
}
