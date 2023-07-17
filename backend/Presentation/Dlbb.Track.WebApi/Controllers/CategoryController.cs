using System.Security.Claims;
using AutoMapper;
using Dlbb.Track.Application.Commands.Categories.Commands.CreateCategory;
using Dlbb.Track.Application.Commands.Categories.Commands.DeleteCategory;
using Dlbb.Track.Application.Commands.Categories.Commands.SaveCategory;
using Dlbb.Track.Application.Commands.Categories.Commands.UpdateCategory;
using Dlbb.Track.Application.Commands.Categories.Queries.GetCategories;
using Dlbb.Track.Application.Commands.Categories.Queries.GetCategory;
using Dlbb.Track.Domain.Enums;
using Dlbb.Track.WebApi.Models.Categories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dlbb.Track.WebApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
	private readonly IMapper _mapper;
	private readonly IMediator _mediator;

	public CategoryController(IMapper mapper, IMediator mediator)
	{
		_mapper = mapper;
		_mediator = mediator;
	}

	[HttpGet("GetAll")]
	public async Task<List<CategoryVM>> GetAll()
	{
		var query = new GetCategoriesQuery();

		return await _mediator.Send(query);
	}

	[HttpGet("{categoryId}")]
	public async Task<CategoryVM> Get(Guid categoryId)
	{
		var query = new GetCategoryQuery() { Id = categoryId };

		return await _mediator.Send(query);
	}

	[Authorize]
	[HttpPost("CreateLocal")]
	public Task<Guid> CreateLocal([FromBody] CreateCategoryDto dto)
	{
		var command = _mapper.Map<CreateCategoryCommand>(dto);

		command.AppUserId = command.AppUserId = Guid.Parse
			(User.Claims.SingleOrDefault(c => ClaimTypes.IsPersistent == c.Type)!.Value);

		command.IsGlobal = false;

		return _mediator.Send(command);
	}

	[Authorize(Policy = nameof(RoleEnum.Admin))]
	[HttpPost("CreateGlobal")]
	public Task<Guid> CreateGlobal([FromBody] CreateCategoryDto dto)
	{
		var command = _mapper.Map<CreateCategoryCommand>(dto);

		command.AppUserId = Guid.Parse
			(User.Claims.SingleOrDefault(c => ClaimTypes.IsPersistent == c.Type)!.Value);

		command.IsGlobal = true;

		return _mediator.Send(command);
	}

	[Authorize]
	[HttpDelete("DeleteLocal: {categoryId}")]
	public Task DeleteLocal(Guid categoryId)
	{
		var command = new DeleteCategoryCommand() { Id = categoryId };

		command.IsGlobal = false;

		return _mediator.Send(command);
	}

	[Authorize(Policy = nameof(RoleEnum.Admin))]
	[HttpDelete("DeleteGlobal: {categoryId}")]
	public Task DeleteGlobal(Guid categoryId)
	{
		var command = new DeleteCategoryCommand() { Id = categoryId };
		command.IsGlobal = true;

		return _mediator.Send(command);
	}

	[Authorize]
	[HttpPut("UpdateLocal")]
	public Task UpdateLocal([FromBody] UpdateCategoryDto dto)
	{
		var command = _mapper.Map<UpdateCategoryCommand>(dto);

		command.IsGlobal = false;

		return _mediator.Send(command);
	}

	[Authorize(Policy = nameof(RoleEnum.Admin))]
	[HttpPut("UpdateGlobal")]
	public Task UpdateGlobal([FromBody] UpdateCategoryDto dto)
	{
		var command = _mapper.Map<UpdateCategoryCommand>(dto);

		command.IsGlobal = true;

		return _mediator.Send(command);
	}

	[Authorize]
	[HttpPut("SaveLocal")]
	public Task SaveLocal([FromBody] SaveCategoryDto dto)
	{
		var command = _mapper.Map<SaveCategoryCommand>(dto);
		command.IsGlobal = false;

		return _mediator.Send(command);
	}

	[Authorize(Policy = nameof(RoleEnum.Admin))]
	[HttpPut("SaveGlobal")]
	public Task SaveGlobal([FromBody] SaveCategoryDto dto)
	{
		var command = _mapper.Map<SaveCategoryCommand>(dto);
		command.IsGlobal = true;

		return _mediator.Send(command);
	}
}
