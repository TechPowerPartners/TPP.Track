using AutoMapper;
using Dlbb.Track.Application.Common.Mappings;
using Dlbb.Track.Persistence.Contexts;

namespace Dlbb.Application.Tests.Common;

public class QueryTestFixture : IDisposable
{
	public AppDbContext Context;
	public IMapper Mapper;

	public QueryTestFixture()
	{
		Context = AppDbContextFactory.Create();

		var configurationProvider = new MapperConfiguration(cfg =>
		{
			cfg.AddProfile(new ApplicationMappingProfile());
		});

		Mapper = configurationProvider.CreateMapper();
	}

	public void Dispose()
	{
		AppDbContextFactory.Destroy(Context);
	}
}

[CollectionDefinition("QueryCollection")]
public class QueryColection : ICollectionFixture<QueryTestFixture> { }
