using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
using Repository.Context;


namespace WebApiPlace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PeopleController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/People
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPerson()
        {
            return await _context.People.Include( x => x.Friends).ToListAsync();
        }

        // GET: api/People/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            var person = await _context.People.Include(x => x.Friends).FirstOrDefaultAsync( x => x.Id == id);

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

        // PUT: api/People/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(int id, Person person)
        {
            if (id != person.Id)
            {
                return BadRequest();
            }

            var personUpdate = _context.People.Find(id);

            personUpdate.Name = person.Name;
            personUpdate.UrlPhoto = person.UrlPhoto;
            personUpdate.Email = person.Email;
            personUpdate.Phone = person.Phone;
            personUpdate.Birthday = person.Birthday;
            personUpdate.CountryId = person.CountryId;
            personUpdate.StateId = person.StateId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // POST: api/People
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {
            _context.People.Add(person);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPerson", new { id = person.Id }, person);
        }

        // DELETE: api/People/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Person>> DeletePerson(int id)
        {
            var person = await _context.People.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            _context.People.Remove(person);
            await _context.SaveChangesAsync();

            return person;
        }


        [HttpGet("{id}/friends")]
        public async Task<ActionResult> GetFriends(int id)
        {
            var person = await _context.People.Include(x => x.Friends).FirstOrDefaultAsync(x => x.Id == id);

            if (person == null) 
            {
                return NotFound();
            }
            return Ok(person);
        }

        // POST: api/People
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("{id}/friends")]
        public async Task<ActionResult> PostFriends([FromRoute] int id, [FromBody] PostFriendsRequest request)
        {
            var person = await _context.People.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }  

            var friends = await _context.People.Where(x => request.Ids.Contains(x.Id)).ToListAsync();

            person.Friends = friends;

            _context.People.Update(person);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool PersonExists(int id)
        {
            return _context.People.Any(e => e.Id == id);
        }

        public class PostFriendsRequest
        {
            public int[] Ids { get; set; }
        }
    }
}
