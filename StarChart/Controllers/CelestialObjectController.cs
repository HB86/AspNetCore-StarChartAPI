using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarChart.Data;
using StarChart.Models;

namespace StarChart.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class CelestialObjectController : ControllerBase
    {

        private ApplicationDbContext _context;

        public CelestialObjectController(ApplicationDbContext context)
        {

            _context = context;


        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CelestialObject>> GetById(int id)
        {
            var celestialObject = await _context.CelestialObjects.FindAsync(id);

            if (celestialObject == null)
            {
                return NotFound();

            }


            return celestialObject;

        }

        [HttpGet("{name}")]
        public async Task <ActionResult<CelestialObject>> GetByName(string Name)
        {
            var celestialObject = await _context.CelestialObjects.FindAsync(Name);


            if (celestialObject == null)
            {
                return NotFound();

            }


            return celestialObject;

        }

    }
}
