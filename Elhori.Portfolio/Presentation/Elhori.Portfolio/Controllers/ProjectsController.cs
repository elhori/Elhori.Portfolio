using System.ComponentModel.DataAnnotations;
using Elhori.Portfolio.Application.Contracts.Services;
using Elhori.Portfolio.Domain.Dtos;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Elhori.Portfolio.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectsController(IProjectService service, IValidator<ProjectDto> validator) : ControllerBase
{
    // GET: api/Projects/page/{page}/size/{size}
    [HttpGet("page/{page}/size/{size}")]
    public async Task<IActionResult> Get(
        [FromQuery] string searchQuery,
        [Range(1, int.MaxValue)] int page,
        [Range(10, int.MaxValue)] int size,
        CancellationToken cancellationToken)
    {
        return Ok(await service.GetPaginatedAsync(searchQuery, page, size, cancellationToken));
    }

    // GET: api/Projects/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        return Ok(await service.GetAsync(id, cancellationToken));
    }

    // POST: api/Projects
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ProjectDto dto, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(dto, cancellationToken);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        return Ok(await service.CreateAsync(dto, cancellationToken));
    }

    // PUT: api/Projects
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] ProjectDto dto, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(dto, cancellationToken);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        return Ok(await service.UpdateAsync(dto, cancellationToken));
    }

    // DELETE: api/Projects/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        return Ok(await service.DeleteAsync(id, cancellationToken));
    }
}