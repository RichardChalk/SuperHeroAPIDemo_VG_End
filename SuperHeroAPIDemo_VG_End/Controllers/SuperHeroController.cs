using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPIDemo_G.Data;
using SuperHeroAPIDemo_G.Models;

namespace SuperHeroAPIDemo_G.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public SuperHeroController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        
        
        private static List<SuperHero> heroes = new List<SuperHero>
        {
            new SuperHero
            {
                Id = 1, Name = "Spiderman", FirstName = "Peter",
                SurName="Parker", City="New York"},
            new SuperHero
            {
                Id = 2,
                Name = "Ironman", FirstName = "Tony", SurName="Stark",
                City="New York"},
        };
        
        // READ ALL -  READ ALL -  READ ALL -  READ ALL -  READ ALL -  READ ALL -  READ ALL //
        // READ ALL -  READ ALL -  READ ALL -  READ ALL -  READ ALL -  READ ALL -  READ ALL //
        // READ ALL -  READ ALL -  READ ALL -  READ ALL -  READ ALL -  READ ALL -  READ ALL //
        // READ ALL -  READ ALL -  READ ALL -  READ ALL -  READ ALL -  READ ALL -  READ ALL //
        [HttpGet]
        //[HttpGet("GetAll")]
        public async Task<ActionResult<List<SuperHero>>> GetAll()
        {
            //return Ok(heroes);
            return Ok(await _dbContext.SuperHeroes.ToListAsync());
        }


        // READ ONE - READ ONE - READ ONE - READ ONE - READ ONE - READ ONE - READ ONE - READ ONE //
        // READ ONE - READ ONE - READ ONE - READ ONE - READ ONE - READ ONE - READ ONE - READ ONE //
        // READ ONE - READ ONE - READ ONE - READ ONE - READ ONE - READ ONE - READ ONE - READ ONE //
        // READ ONE - READ ONE - READ ONE - READ ONE - READ ONE - READ ONE - READ ONE - READ ONE //
        [HttpGet]
        [Route("{id}")]
        //[Route("GetOne/{id:int}")]
        public async Task<ActionResult<SuperHero>> GetOne(int id)
        {
            //var hero = heroes.Find(s => s.Id == id);
            var hero = _dbContext.SuperHeroes.Find(id);

            if (hero == null)
            {
                return BadRequest("Superhero not found");
            }
            return Ok(hero);
        }

        // POST - POST - POST - POST - POST - POST - POST - POST - POST - POST - POST - POST - POST //
        // POST - POST - POST - POST - POST - POST - POST - POST - POST - POST - POST - POST - POST //
        // POST - POST - POST - POST - POST - POST - POST - POST - POST - POST - POST - POST - POST //
        // POST - POST - POST - POST - POST - POST - POST - POST - POST - POST - POST - POST - POST //
        [HttpPost]
        public async Task<ActionResult<SuperHero>> PostHero(SuperHero hero)
        {
            //heroes.Add(hero);
            _dbContext.SuperHeroes.Add(hero);
            await _dbContext.SaveChangesAsync();
            return Ok(await _dbContext.SuperHeroes.ToListAsync());
        }

        // PUT - PUT - PUT - PUT - PUT - PUT - PUT - PUT - PUT - PUT - PUT - PUT - PUT - PUT //
        // PUT - PUT - PUT - PUT - PUT - PUT - PUT - PUT - PUT - PUT - PUT - PUT - PUT - PUT //
        // PUT - PUT - PUT - PUT - PUT - PUT - PUT - PUT - PUT - PUT - PUT - PUT - PUT - PUT //
        // PUT - PUT - PUT - PUT - PUT - PUT - PUT - PUT - PUT - PUT - PUT - PUT - PUT - PUT //
        [HttpPut]
        public async Task<ActionResult<SuperHero>> UpdateHero(SuperHero hero)
        {
            // OBS: PUT Uppdaterar HELA SuperHero (ALLA properties)
            var heroToUpdate = await _dbContext.SuperHeroes.FindAsync(hero.Id);

            if (heroToUpdate == null)
            {
                return BadRequest("Superhero not found");
            }
            
            heroToUpdate.Name = hero.Name;
            heroToUpdate.FirstName = hero.FirstName;
            heroToUpdate.SurName = hero.SurName;
            heroToUpdate.City= hero.City;

            await _dbContext.SaveChangesAsync();
            
            return Ok(await _dbContext.SuperHeroes.ToListAsync());
        }

        // DELETE - DELETE - DELETE - DELETE - DELETE - DELETE - DELETE - DELETE - DELETE //
        // DELETE - DELETE - DELETE - DELETE - DELETE - DELETE - DELETE - DELETE - DELETE //
        // DELETE - DELETE - DELETE - DELETE - DELETE - DELETE - DELETE - DELETE - DELETE //
        // DELETE - DELETE - DELETE - DELETE - DELETE - DELETE - DELETE - DELETE - DELETE //
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<SuperHero>> Delete(int id)
        {
            var hero = await _dbContext.SuperHeroes.FindAsync(id);

            if (hero == null)
            {
                return BadRequest("Superhero not found");
            }

            _dbContext.SuperHeroes.Remove(hero);
            await _dbContext.SaveChangesAsync();
            return Ok(await _dbContext.SuperHeroes.ToListAsync());
        }
    }
}
