using AutoMapper;
using Domain.Models;
using Persistence.Identity;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Department, Department>();
            CreateMap<ApplicationUser, ApplicationUser>();
        }
    }
}