using AutoMapper;
using Dlbb.Track.Application.Accounts.Queries.Login;
using Dlbb.Track.Application.Activities.Queries.GetActivity;
using Dlbb.Track.Domain.Entities;

namespace Dlbb.Track.Application.Common.Mappings;
public class AssemblyMappingProfile : Profile
{
	public AssemblyMappingProfile()
	{
		ApplyMappings();
	}

	private void ApplyMappings()
	{
		CreateMap<Activity, ActivityVm>()
			.ForMember(aVm => aVm.Name,
				opt => opt.MapFrom(a => a.Name))
			.ForMember(aVm => aVm.Id,
				opt => opt.MapFrom(a => a.Id))
			.ForMember(aVm => aVm.Description,
				opt => opt.MapFrom(a => a.Description));
	}
}
