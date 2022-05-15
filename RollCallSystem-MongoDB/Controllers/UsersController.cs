
using Microsoft.AspNetCore.Mvc;
using RollCallSystem_MongoDB.Models;
using RollCallSystem_MongoDB.Services;

namespace RollCallSystem_MongoDB.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UsersService _UsersService;

    public UsersController(UsersService UsersService) =>
        _UsersService = UsersService;

    [HttpGet]
    public async Task<List<User>> Get() =>
        await _UsersService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<User>> Get(string id)
    {
        var User = await _UsersService.GetAsync(id);

        if (User is null)
        {
            return NotFound();
        }

        return User;
    }

    [HttpPost]
    public async Task<IActionResult> Post(User newUser)
    {
        await _UsersService.CreateAsync(newUser);

        return CreatedAtAction(nameof(Get), new { id = newUser.Id }, newUser);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, User updatedUser)
    {
        var User = await _UsersService.GetAsync(id);

        if (User is null)
        {
            return NotFound();
        }

        updatedUser.Id = User.Id;

        await _UsersService.UpdateAsync(id, updatedUser);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var User = await _UsersService.GetAsync(id);

        if (User is null)
        {
            return NotFound();
        }

        await _UsersService.RemoveAsync(id);

        return NoContent();
    }
}
