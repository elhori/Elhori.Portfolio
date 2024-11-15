using System.ComponentModel.DataAnnotations;
using Elhori.Portfolio.Application.Contracts.Services;
using Elhori.Portfolio.Domain.Dtos;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Elhori.Portfolio.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InformationController(IInfoService service, IValidator<InfoDto> validator) : ControllerBase
{
    // GET: api/Information
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        return Ok(await service.GetAsync(cancellationToken));
    }

    // POST: api/Information
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] InfoDto dto, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(dto, cancellationToken);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        return Ok(await service.CreateAsync(dto, cancellationToken));
    }

    // PUT: api/Information
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] InfoDto dto, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(dto, cancellationToken);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        return Ok(await service.UpdateAsync(dto, cancellationToken));
    }

    // DELETE: api/Information/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        return Ok(await service.DeleteAsync(id, cancellationToken));
    }
}