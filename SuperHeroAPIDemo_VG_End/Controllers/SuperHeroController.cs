using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPIDemo_G.Data;
using SuperHeroAPIDemo_G.Models;
using System.Data;

namespace SuperHeroAPIDemo_G.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class SuperHeroController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public SuperHeroController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        // READ ALL -  READ ALL -  READ ALL -  READ ALL -  READ ALL -  READ ALL -  READ ALL //
        // READ ALL -  READ ALL -  READ ALL -  READ ALL -  READ ALL -  READ ALL -  READ ALL //
        // READ ALL -  READ ALL -  READ ALL -  READ ALL -  READ ALL -  READ ALL -  READ ALL //
        // READ ALL -  READ ALL -  READ ALL -  READ ALL -  READ ALL -  READ ALL -  READ ALL //

        // READ ALL ///////////////////////////////////////////////////////
        /// <summary>
        /// Retrieve ALL Superheroes from the database
        /// </summary>
        /// <returns>
        /// A full list of ALL Superheroes
        /// </returns>
        /// <remarks>
        /// Example end point: GET /api/SuperHero
        /// </remarks>
        /// <response code="200">
        /// Successfully returned a full list of ALL Superheroes
        /// </response>
        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<List<SuperHero>>> GetAll()
        {
            //return Ok(heroes);
            return Ok(await _dbContext.SuperHeroes.ToListAsync());
        }


        // READ ONE - READ ONE - READ ONE - READ ONE - READ ONE - READ ONE - READ ONE - READ ONE //
        // READ ONE - READ ONE - READ ONE - READ ONE - READ ONE - READ ONE - READ ONE - READ ONE //
        // READ ONE - READ ONE - READ ONE - READ ONE - READ ONE - READ ONE - READ ONE - READ ONE //
        // READ ONE - READ ONE - READ ONE - READ ONE - READ ONE - READ ONE - READ ONE - READ ONE //

        /// <summary>
        /// Retrieve a SPECIFIC Superhero from the database
        /// </summary>
        /// <param name="id">
        /// Id of specific Superhero
        /// </param>
        /// <returns>
        /// The chosen Superhero (by Id)
        /// </returns>
        /// <remarks>
        /// Example end point: GET /api/SuperHero/1
        /// </remarks>
        /// <response code="200">
        /// Successfully returned the chosen SuperHero (by Id)
        /// </response>
        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<SuperHero>> GetOne(int id)
        {
            //var hero = heroes.Find(s => s.Id == id);
            var hero = await _dbContext.SuperHeroes.FindAsync(id);

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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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

        // PATCH - PATCH - PATCH - PATCH - PATCH - PATCH - PATCH - PATCH - PATCH - PATCH //
        // PATCH - PATCH - PATCH - PATCH - PATCH - PATCH - PATCH - PATCH - PATCH - PATCH //
        // PATCH - PATCH - PATCH - PATCH - PATCH - PATCH - PATCH - PATCH - PATCH - PATCH //
        // PATCH - PATCH - PATCH - PATCH - PATCH - PATCH - PATCH - PATCH - PATCH - PATCH //
        [HttpPatch]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<SuperHero>> PatchHero(JsonPatchDocument hero, int id)
        {
            // OBS: PUT Uppdaterar SuperHero (VISSA properties)
            var heroToUpdate = await _dbContext.SuperHeroes.FindAsync(id);

            if (heroToUpdate == null)
            {
                return BadRequest("Superhero not found");
            }

            hero.ApplyTo(heroToUpdate);
            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.SuperHeroes.ToListAsync());
        }
    }
}
