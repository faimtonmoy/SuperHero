using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHero.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    { 
            
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Superhero>>> Get()
        {

            return Ok(await _context.Superheroes.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Superhero>> Get(int id)
        {
            var findHero = await _context.Superheroes.FindAsync(id);
            if (findHero == null)
                return BadRequest("Hero not found");
            return Ok(findHero);
        }
        [HttpPost]
        public async Task<ActionResult> Post(Superhero post)
        {
            _context.Superheroes.Add(post);
            await _context.SaveChangesAsync();
            return Ok(await _context.Superheroes.ToListAsync());
        }
        [HttpPut]
        public async Task<ActionResult<Superhero>> UpdateHero(Superhero request)
        {
            var findHero = await _context.Superheroes.FindAsync(request.id);
            if (findHero == null)
                return BadRequest("Hero not found");
            findHero.name= request.name;
            findHero.firstName= request.firstName;
            findHero.lastName= request.lastName;
            findHero.place = request.place;

            await _context.SaveChangesAsync();
            return Ok(await _context.Superheroes.ToListAsync());
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Superhero>> Delete(int id)
        {
            var findHero = await _context.Superheroes.FindAsync(id);
            if (findHero == null)
                return BadRequest("Hero not found");
            _context.Superheroes.Remove(findHero);
            await _context.SaveChangesAsync();
            return Ok(await _context.Superheroes.ToListAsync());
        }
    }
}
