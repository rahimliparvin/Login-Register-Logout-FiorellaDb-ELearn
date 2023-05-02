﻿using ELEARN.Data;
using ELEARN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ELEARN.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        public SliderController(AppDbContext context)
        {
            _context = context;
        }

       
        public async Task<IActionResult> Index()
        {

            IEnumerable<Slider> sliders = await _context.Sliders.Where(m => !m.SoftDelete).ToListAsync();

            return View(sliders);
        }

        public async Task<IActionResult> Detail(int? id)
        {

            if (id == null) return BadRequest();
           
            Slider slider = await _context.Sliders.Where(m => m.Id == id).FirstOrDefaultAsync();

            if (slider == null) return NotFound();

            return View(slider);
        }

        public IActionResult Create()
        {


            return View();
        }



    }
}
