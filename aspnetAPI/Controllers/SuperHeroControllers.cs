using aspnetAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace aspnetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroControllers : ControllerBase
    {
        private readonly DataContext _dataContext;

        public SuperHeroControllers(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        // [Route("SuperHero")]
        // first version
        // public async Task<IActionResult> Get()
        // second version ->..
       /* private static List<SuperHero> heroes = new List<SuperHero>
            {
                *//*new SuperHero
                {
                    Id = 1,
                    Name = "Alijonov Muhammadqodir",
                    LastName  = "Alijonov",
                    FirstName = "Muhammadqodir",
                    Place = "Namangan City"
                },*//*
                new SuperHero
                {
                    Id = 2,
                    Name = "Komilov Izzat",
                    LastName  = "Komilov",
                    FirstName = "Izzat",
                    Place = "Namangan City"
                }
            };
*/
       
        // return Ok(heroes);
        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
           return Ok(await _dataContext.SuperHeroes.ToListAsync());
        }

        

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
           var hero =  await _dataContext.SuperHeroes.FindAsync(id);
            if(hero == null)
                return BadRequest("hero not found. ");
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _dataContext.SuperHeroes.Add(hero);
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.SuperHeroes.ToListAsync() );
        }
        [HttpPut]

        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var dbhero = await _dataContext.SuperHeroes.FindAsync(request.Id);
            if (dbhero == null)
                return BadRequest("hero not found. ");

            dbhero.Name = request.Name;
            dbhero.FirstName = request.FirstName;
            dbhero.Place = request.Place;
            dbhero.LastName = request.LastName;

            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.SuperHeroes.ToListAsync());
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {
            var dbhero = await _dataContext.SuperHeroes.FindAsync(id);
            if (dbhero == null)
                return BadRequest("hero not found. ");

            _dataContext.SuperHeroes.Remove(dbhero);
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.SuperHeroes.ToListAsync());
        }

    }
}
