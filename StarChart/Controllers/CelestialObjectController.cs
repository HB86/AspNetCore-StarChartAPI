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


    [Route("")]
    [ApiController]

    public class CelestialObjectController : ControllerBase
    {

        private readonly ApplicationDbContext _context;



        public CelestialObjectController(ApplicationDbContext context)
        {

            _context = context;


        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var celestialObject = _context.CelestialObjects.FirstOrDefault(c => c.Id == id);


            if (celestialObject == null)
            {
                return NotFound();

            }


            celestialObject.Satellites = _context.CelestialObjects.Where(c => c.OrbitedObjectId == id).ToList();

            return Ok(celestialObject);

        }

        [HttpGet("{name}")]
        public IActionResult GetByName(string Name)

        {
            var celestials = _context.CelestialObjects.Where(c => c.Name == Name).ToList();


            if (!celestials.Any())
            {

                return NotFound();


            }


            foreach (var celestialObject in celestials)
            {

                celestialObject.Satellites = _context.CelestialObjects.Where(c => c.OrbitedObjectId == celestialObject.Id).ToList();


            }


            return Ok(celestials);

        }

        [HttpGet]
        public IActionResult GetAll()
        {

            var celestials = new List<CelestialObject>();

            celestials = _context.CelestialObjects.ToList();

            foreach (var celestialObject in celestials)
            {

                celestialObject.Satellites = _context.CelestialObjects.Where
                    (c => c.OrbitedObjectId == celestialObject.Id).ToList();


            }

            if (celestials.Count == 0)
            {

                return NotFound();

            }

            return Ok(celestials);




        }

        [HttpPost]
        public IActionResult Create([FromBody] CelestialObject celestialObject)
        {
            _context.Add(celestialObject);
            _context.SaveChanges();

            return CreatedAtRoute(
                "GetById", new { id = celestialObject.Id }, celestialObject
                 );



        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, CelestialObject celestialObject)
        {
            var existingObject = _context.CelestialObjects.Find(id == celestialObject.Id);


            if (celestialObject == null)
            {

                return NotFound();

            }


            existingObject.Name = celestialObject.Name;
            existingObject.OrbitedObjectId = celestialObject.OrbitedObjectId;
            existingObject.OrbitalPeriod = celestialObject.OrbitalPeriod;


            _context.CelestialObjects.Update(existingObject);
            _context.SaveChanges();

            return NoContent();



        }


        [HttpPatch ("{id}/{name}")]
        public IActionResult RenameObject(int id, string name)
        {
            var existingObject = _context.CelestialObjects.Find(id);

            if(existingObject == null)
            {

                return NotFound();
            }


            existingObject.Name = name;
            _context.Update(existingObject);
            _context.SaveChanges();

            return NoContent();

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var celestialObjects = _context.CelestialObjects.Where(c => c.Id == id || c.OrbitedObjectId == id).ToList();

            if (!celestialObjects.Any())
            {
                return NotFound();

            }


            _context.CelestialObjects.RemoveRange(celestialObjects);
            _context.SaveChanges();

            return NoContent();

        }


    }

}

