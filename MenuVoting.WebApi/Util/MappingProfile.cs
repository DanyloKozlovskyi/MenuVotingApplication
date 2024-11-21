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
				.ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));

			CreateMap<VoteCreate, Vote>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
				.ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
				.ForMember(dest => dest.MenuId, opt => opt.MapFrom(src => src.MenuId));

			CreateMap<MenuCreate, Menu>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
				.ForMember(dest => dest.Components, opt => opt.MapFrom(src => src.Components))
				.ForMember(dest => dest.MenuPoolId, opt => opt.MapFrom(src => src.MenuPoolId));
		}
	}
}
