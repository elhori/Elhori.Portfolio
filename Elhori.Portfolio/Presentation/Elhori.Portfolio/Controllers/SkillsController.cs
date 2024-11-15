using Elhori.Portfolio.Application.Contracts.Services;
using Elhori.Portfolio.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Elhori.Portfolio.Domain.Dtos;

namespace Elhori.Portfolio.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SkillsController(ISkillService service, IValidator<SkillDto> validator) : ControllerBase
{
    // GET: api/Skills/page/{page}/size/{size}
    [HttpGet("page/{page}/size/{size}")]
    public async Task<IActionResult> Get(
        [FromQuery] string searchQuery,
        [Range(1, int.MaxValue)] int page, 
        [Range(10, int.MaxValue)] int size, 
        CancellationToken cancellationToken)
    {
        return Ok(await service.GetPaginatedAsync(searchQuery, page, size, cancellationToken));
    }

    // GET: api/Skills/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        return Ok(await service.GetAsync(id, cancellationToken));
    }

    // POST: api/Skills
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] SkillDto dto, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(dto, cancellationToken);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        return Ok(await service.CreateAsync(dto, cancellationToken));
    }

    // PUT: api/Skills
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] SkillDto dto, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(dto, cancellationToken);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        return Ok(await service.UpdateAsync(dto, cancellationToken));
    }

    // DELETE: api/Skills/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        return Ok(await service.DeleteAsync(id, cancellationToken));
    }
}