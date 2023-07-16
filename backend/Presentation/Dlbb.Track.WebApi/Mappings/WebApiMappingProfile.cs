using AutoMapper;
using Dlbb.Track.Application.Accounts.Commands.Register;
using Dlbb.Track.Application.Accounts.Queries.Login;
using Dlbb.Track.Application.Activities.Commands.CreateActivity;
using Dlbb.Track.Application.Activities.Commands.UpdateActivity;
using Dlbb.Track.Application.Sessions.Commands.CreateSession;
using Dlbb.Track.Application.Sessions.Commands.EndSession;
using Dlbb.Track.WebApi.Models.Account;
using Dlbb.Track.WebApi.Models.Activities;
using Dlbb.Track.WebApi.Models.Sessions;

namespace Dlbb.Track.WebApi.Mappings;

public class WebApiMappingProfile : Profile
{
	public WebApiMappingProfile()
	{
		ApplyMappings();
	}

	private void ApplyMappings()
	{
		CreateMap<CreateActivityDto, CreateActivityCommand>()
			.ForMember(aCommand=>aCommand.Name,
				opt=>opt.MapFrom(aDto=>aDto.Name))
			.ForMember(aCommand=>aCommand.Description,
				opt=>opt.MapFrom(aDto=>aDto.Description))
			.ForMember(aCommand => aCommand.Claims,
				opt => opt.MapFrom(ac => ac.Claims));

		CreateMap<UpdateActivityDto, UpdateActivityCommand>()
			.ForMember(aCommand => aCommand.Id,
				opt => opt.MapFrom(aDto => aDto.Id))
			.ForMember(aCommand => aCommand.Name,
				opt => opt.MapFrom(aDto => aDto.Name))
			.ForMember(aCommand => aCommand.Description,
				opt => opt.MapFrom(aDto => aDto.Description))
			.ForMember(aCommand => aCommand.Claims,
				opt => opt.MapFrom(ac => ac.Claims));

		CreateMap<CreateSessionDto, CreateSessionCommand>();

		CreateMap<EndSessionDto, EndSessionCommand>();

		CreateMap<RegisterDto, RegisterUserCommand>();

		CreateMap<LoginVm, LoginUserQuery>()
			.ForMember(lq => lq.ExpectedEmail, opt => opt.MapFrom(vm => vm.Email))
			.ForMember(lq => lq.ExpectedPassword, opt => opt.MapFrom(vm => vm.Password));
	}
}
