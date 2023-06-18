using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dlbb.Track.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
	private readonly IMediator _mediator;
	private readonly IMapper _mapper;

	public AccountController(IMediator mediator, IMapper mapper)
	{
		_mediator = mediator;
		_mapper = mapper;
	}

	[HttpGet]
	public IEnumerable<string> Get()
	{
		return new string[] { "value1", "value2" };
	}


	[HttpGet("{id}")]
	public string Get(int id)
	{
		return "value";
	}


	[HttpPost]
	public void Post([FromBody] string value)
	{
	}


	[HttpPut("{id}")]
	public void Put(int id, [FromBody] string value)
	{
	}


	[HttpDelete("{id}")]
	public void Delete(int id)
	{
	}
}
