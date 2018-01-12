using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    public class PetsController : Controller
    {
        static IDictionary<int, Pet> _pets =
            new string[] { "Fluffy", "Fido", "Spot", "Wally the Wonder Fish" }
            .Select((name, id) => new Pet { Id = id, Name = name })
            .ToDictionary(pet => pet.Id);
        static bool fail = false;

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Pet> Get()
        {
            //fail = !fail;
            if (fail) throw new InvalidOperationException("Oops!");
            return _pets.Values;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get([FromRoute]int id)
        {
            if (!_pets.TryGetValue(id, out var pet))
            {
                return NotFound();
            }
            return Ok(pet);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Pet pet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            pet.Id = _pets.Keys.Max() + 1;
            _pets[pet.Id] = pet;

            return CreatedAtAction(nameof(Get), new { id = pet.Id }, pet);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put([FromRoute]int id, [FromBody]Pet pet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_pets.ContainsKey(id))
            {
                return NotFound();
            }

            _pets[id] = pet;

            return Ok(pet);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute]int id)
        {
            if (!_pets.Remove(id, out var pet))
            {
                return NotFound();
            }

            return Ok(pet);
        }
    }
}
