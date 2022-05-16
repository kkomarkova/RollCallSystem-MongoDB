using Microsoft.AspNetCore.Mvc;
using RollCallSystem_MongoDB.Models;
using RollCallSystem_MongoDB.Services;

namespace RollCallSystem_MongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly SubjectsService _SubjectsService;

        public SubjectsController(SubjectsService SubjectsService) =>
            _SubjectsService = SubjectsService;

        [HttpGet]
        public async Task<List<Subject>> Get() =>
            await _SubjectsService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Subject>> Get(string id)
        {
            var Subject = await _SubjectsService.GetAsync(id);

            if (Subject is null)
            {
                return NotFound();
            }

            return Subject;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Subject newSubject)
        {
            await _SubjectsService.CreateAsync(newSubject);

            return CreatedAtAction(nameof(Get), new { id = newSubject.Id }, newSubject);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Subject updatedSubject)
        {
            var Subject = await _SubjectsService.GetAsync(id);

            if (Subject is null)
            {
                return NotFound();
            }

            updatedSubject.Id = Subject.Id;

            await _SubjectsService.UpdateAsync(id, updatedSubject);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var Subject = await _SubjectsService.GetAsync(id);

            if (Subject is null)
            {
                return NotFound();
            }

            await _SubjectsService.RemoveAsync(id);

            return NoContent();
        }
    }
}
