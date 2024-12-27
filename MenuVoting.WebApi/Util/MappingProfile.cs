using AutoMapper;
using MenuVoting.DataAccess.Dtos;
using MenuVoting.DataAccess.Identity;
using MenuVoting.DataAccess.Models;

namespace MenuVoting.WebApi.Util
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterDto, ApplicationUser>()
                .ForMember(dest => dest.PersonName, opt => opt.MapFrom(src => src.PersonName))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.RestaurantId, opt => opt.MapFrom(src => src.RestaurantId))
                .ForMember(dest => dest.IsAdmin, opt => opt.MapFrom(src => src.IsAdmin));

            CreateMap<VoteCreate, Vote>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.MenuId, opt => opt.MapFrom(src => src.MenuId));

            CreateMap<MenuCreate, Menu>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.Dishes, opt => opt.MapFrom(src => src.Components))
                .ForMember(dest => dest.MenuPoolId, opt => opt.MapFrom(src => src.MenuPoolId));

            CreateMap<RestaurantCreate, Restaurant>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));

            CreateMap<MenuPoolCreate, MenuPool>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.RestaurantId, opt => opt.MapFrom(src => src.RestaurantId))
                .ForMember(dest => dest.Menus, opt => opt.MapFrom(src => new List<Menu>())).
                ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateOnly.FromDateTime(DateTime.UtcNow)));
        }
    }
}
