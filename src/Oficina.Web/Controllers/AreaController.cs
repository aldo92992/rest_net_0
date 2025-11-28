using Microsoft.AspNetCore.Mvc;
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
}
