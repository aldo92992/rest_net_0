using Microsoft.AspNetCore.Mvc;
using Oficina.Application.DTOs;
using Oficina.Application.Interfaces;

namespace Oficina.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmpleadoController : ControllerBase
{
    private readonly IEmpleadoService _empleadoService;

    public EmpleadoController(IEmpleadoService empleadoService)
    {
        _empleadoService = empleadoService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var empleados = await _empleadoService.GetAllAsync(ct);
        return Ok(empleados);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var empleado = await _empleadoService.GetByIdAsync(id, ct);
        if (empleado is null) return NotFound();
        return Ok(empleado);
    }

    [HttpGet("area/{areaId:int}")]
    public async Task<IActionResult> GetByArea(int areaId, CancellationToken ct)
    {
        var empleados = await _empleadoService.GetByAreaAsync(areaId, ct);
        return Ok(empleados);
    }


    [HttpGet("name/{name}")]
    public async Task<IActionResult> GetByName(String name, CancellationToken ct)
    {
        var empleados = await _empleadoService.GetByNameAsync(name, ct);
        return Ok(empleados);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEmpleadoRequest request, CancellationToken ct)
    {
        try
        {
            var create = await _empleadoService.CreateAsync(request, ct);
            return CreatedAtAction(nameof(GetById), new { id = create.Id }, create);

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

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateEmpleadoRequest request, CancellationToken ct)
    {
        try
        {
            var updated = await _empleadoService.UpdateAsync(id, request, ct);
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
        var deleted = await _empleadoService.DeleteAsync(id, ct);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
