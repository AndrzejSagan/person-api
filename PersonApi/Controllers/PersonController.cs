

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersonApi.Models;
using PersonApi.Services;

namespace PersonApi.Controllers
{
    [Route("api/[controller]")]
    public class PersonController : Controller
    {
        private readonly IPersonService _service;


        public PersonController(IPersonService service)
        {
            _service = service;

        }


        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPeople()
        {
            var result = await _service.GetAll();

              return result != null ? Ok(result) : (NoContent() as ActionResult);

        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(long id)
        {
            var result = await _service.GetById(id);

            return result != null ? Ok(result) : (NotFound() as ActionResult); 

        }

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson([FromBody]Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.Add(person);

            return CreatedAtAction(nameof(GetPerson), new { id = person.id }, person);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(long id, [FromBody]Person person)
        {

           var result = await _service.Update(id, person);

           return result ? NoContent() : BadRequest() as IActionResult;

        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(long id)
        {
            var result = await _service.Remove(id);

            return result ? NoContent() : NotFound() as IActionResult;
       
        }
    }
}
