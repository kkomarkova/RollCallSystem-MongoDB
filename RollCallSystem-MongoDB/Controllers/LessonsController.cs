using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RollCallSystem_MongoDB.Models;
using RollCallSystem_MongoDB.Services;

namespace RollCallSystem_MongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly LessonsService _LessonsService;

        public LessonsController(LessonsService LessonsService) =>
            _LessonsService = LessonsService;

        [HttpGet]
        public async Task<List<Lesson>> Get() =>
            await _LessonsService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Lesson>> Get(string id)
        {
            var Lesson = await _LessonsService.GetAsync(id);

            if (Lesson is null)
            {
                return NotFound();
            }

            return Lesson;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Lesson newLesson)
        {
            await _LessonsService.CreateAsync(newLesson);

            return CreatedAtAction(nameof(Get), new { id = newLesson.Id }, newLesson);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Lesson updatedLesson)
        {
            var Lesson = await _LessonsService.GetAsync(id);

            if (Lesson is null)
            {
                return NotFound();
            }

            updatedLesson.Id = Lesson.Id;

            await _LessonsService.UpdateAsync(id, updatedLesson);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var Lesson = await _LessonsService.GetAsync(id);

            if (Lesson is null)
            {
                return NotFound();
            }

            await _LessonsService.RemoveAsync(id);

            return NoContent();
        }
    }
}
