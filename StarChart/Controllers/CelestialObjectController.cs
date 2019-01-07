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


            return Ok(celestialObject);

        }

        [HttpGet("{name}")]
        public IActionResult GetByName(string Name)
        {
            var celestialObject = _context.CelestialObjects.FirstOrDefault(c => c.Name == Name);


            if (celestialObject == null)
            {
                return NotFound();

            }


            return Ok(celestialObject.Name);

        }

      [HttpGet]
      public IActionResult GetAll()
        {

            List<CelestialObject> celestials = null;


            celestials = _context.CelestialObjects.ToList();

            foreach(var celestial in celestials)
            {

                celestial.Satellites.Add(celestial);

            }


            if(celestials.Count == 0)
            {

                return NotFound();

            }

            return Ok(celestials);




        }


}

}

