using AutoMapper;
using Dlbb.Track.Application.Accounts.Commands.Register;
using Dlbb.Track.Application.Accounts.Queries.Login;
using Dlbb.Track.Application.Activities.Commands.CreateActivity;
using Dlbb.Track.Application.Activities.Commands.UpdateActivity;
using Dlbb.Track.Application.Commands.Categories.Commands.CreateCategory;
using Dlbb.Track.Application.Commands.Categories.Commands.DeleteCategory;
using Dlbb.Track.Application.Commands.Categories.Commands.SaveCategory;
using Dlbb.Track.Application.Commands.Categories.Commands.UpdateCategory;
using Dlbb.Track.Application.Sessions.Commands.CreateSession;
using Dlbb.Track.Application.Sessions.Commands.EndSession;
using Dlbb.Track.WebApi.Models.Account;
using Dlbb.Track.WebApi.Models.Activities;
using Dlbb.Track.WebApi.Models.Categories;
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
		CreateMap<CreateActivityDto, CreateActivityCommand>(MemberList.Source);	

		CreateMap<UpdateActivityDto, UpdateActivityCommand>(MemberList.Source)
			.ForMember(aCommand => aCommand.Id,
				opt => opt.MapFrom(aDto => aDto.Id))
			.ForMember(aCommand => aCommand.Name,
				opt => opt.MapFrom(aDto => aDto.Name))
			.ForMember(aCommand => aCommand.Description,
				opt => opt.MapFrom(aDto => aDto.Description));

		CreateMap<CreateSessionDto, CreateSessionCommand>(MemberList.Source);

		CreateMap<EndSessionDto, EndSessionCommand>();

		CreateMap<RegisterDto, RegisterUserCommand>();

		CreateMap<LoginVm, LoginUserQuery>()
			.ForMember(lq => lq.ExpectedEmail, opt => opt.MapFrom(vm => vm.Email))
			.ForMember(lq => lq.ExpectedPassword, opt => opt.MapFrom(vm => vm.Password));

		CreateMap<CreateCategoryDto, CreateCategoryCommand>(MemberList.Source);

		CreateMap<UpdateCategoryDto, UpdateCategoryCommand>(MemberList.Source);

		CreateMap<SaveCategoryDto, SaveCategoryCommand>(MemberList.Source);
	}
}
