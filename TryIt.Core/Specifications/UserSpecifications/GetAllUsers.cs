using Ardalis.Specification;
using TryIt.Core.Entities;

namespace TryIt.Core.Specifications.UserSpecifications
{
    public class GetAllUsers : Specification<User>
    {
        public GetAllUsers(int pageSize,int pageNumber)
        {
            Query.Take(pageSize).Skip(pageNumber);
        }
    }
}
