using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RollCallSystem_MongoDB.Models;
using RollCallSystem_MongoDB.Services;

namespace RollCallSystem_MongoDB.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TrophiesController : ControllerBase
{
    private readonly TrophiesService _TrophiesService;

    public TrophiesController(TrophiesService TrophiesService) =>
        _TrophiesService = TrophiesService;

    [HttpGet]
    public async Task<List<Trophy>> Get() =>
        await _TrophiesService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Trophy>> Get(string id)
    {
        var Trophy = await _TrophiesService.GetAsync(id);

        if (Trophy is null)
        {
            return NotFound();
        }

        return Trophy;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Trophy newTrophy)
    {
        await _TrophiesService.CreateAsync(newTrophy);

        return CreatedAtAction(nameof(Get), new { id = newTrophy.Id }, newTrophy);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Trophy updatedTrophy)
    {
        var Trophy = await _TrophiesService.GetAsync(id);

        if (Trophy is null)
        {
            return NotFound();
        }

        updatedTrophy.Id = Trophy.Id;

        await _TrophiesService.UpdateAsync(id, updatedTrophy);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var Trophy = await _TrophiesService.GetAsync(id);

        if (Trophy is null)
        {
            return NotFound();
        }

        await _TrophiesService.RemoveAsync(id);

        return NoContent();
    }
}
