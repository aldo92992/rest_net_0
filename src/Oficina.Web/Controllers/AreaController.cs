using Microsoft.AspNetCore.Mvc;
using Oficina.Application.DTOs;
using Oficina.Application.Interfaces;

namespace Oficina.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AreaController : ControllerBase
{
    private readonly IAreaService _areaService;

    public AreaController(IAreaService areaService)
    {
        _areaService = areaService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var areas = await _areaService.GetAllAsync(ct);
        return Ok(areas);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAreaRequest request, CancellationToken ct)
    {
        try
        {
            var create = await _areaService.CreateAsync(request, ct);
            return CreatedAtAction(nameof(GetAll), new { id = create.Id }, create);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateAreaRequest request, CancellationToken ct)
    {
        try
        {
            var updated = await _areaService.UpdateAsync(id, request, ct);
            if (!updated) return NotFound();
            return Ok(updated);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { error = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var deleted = await _areaService.DeleteAsync(id, ct);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
