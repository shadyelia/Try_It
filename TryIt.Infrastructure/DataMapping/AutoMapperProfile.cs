using AutoMapper;
using TryIt.Core.DTOs;
using TryIt.Core.Entities;

namespace TryIt.Infrastructure.DataMapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, AuthenticateResponseDTO>();
            CreateMap<RegisterRequestDTO, User>();

            CreateMap<UpdateRequestDTO, User>()
            .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    // ignore null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    return true;
                }
            ));
        }
    }
}
