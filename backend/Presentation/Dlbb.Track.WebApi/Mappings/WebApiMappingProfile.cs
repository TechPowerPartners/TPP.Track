using AutoMapper;
using Dlbb.Track.Application.Activities.Commands.CreateActivity;
using Dlbb.Track.Application.Activities.Commands.UpdateActivity;
using Dlbb.Track.WebApi.Models;

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
				opt=>opt.MapFrom(aDto=>aDto.Description));

		CreateMap<UpdateActivityDto, UpdateActivityCommand>()
			.ForMember(aCommand => aCommand.Id,
				opt => opt.MapFrom(aDto => aDto.Id))
			.ForMember(aCommand => aCommand.Name,
				opt => opt.MapFrom(aDto => aDto.Name))
			.ForMember(aCommand => aCommand.Description,
				opt => opt.MapFrom(aDto => aDto.Description));
	}
}
