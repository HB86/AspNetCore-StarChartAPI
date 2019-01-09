﻿using System;
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


            if(!celestials.Any())
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

            foreach(var celestialObject in celestials)
            {

                celestialObject.Satellites = _context.CelestialObjects.Where
                    (c => c.OrbitedObjectId == celestialObject.Id).ToList();


            }

            if(celestials.Count == 0)
            {

                return NotFound();

            }

            return Ok(celestials);




        }


}

}

