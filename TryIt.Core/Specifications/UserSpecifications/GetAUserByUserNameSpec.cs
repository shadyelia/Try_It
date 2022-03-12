using Ardalis.Specification;
using TryIt.Core.Entities;

namespace TryIt.Core.Specifications.UserSpecifications
{
    public class GetAUserByUserNameSpec : Specification<User> ,ISingleResultSpecification
    {
        public GetAUserByUserNameSpec(string userName)
        {
            Query.Where(user => user.Username == userName);
        }
    }
}
