using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Lego_Inventory.Data;
using Lego_Inventory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace Lego_Inventory.Controllers
{
    
    public class LegoController : Controller
    {
        private readonly ILegoRepository _legoRepository;

        public LegoController(ILegoRepository legoRespository)
        {
            _legoRepository = legoRespository;
        }
        // Landing page
        public IActionResult Index()
        {
            return View();
        }
        // List page: shows all LEGO sets
        public async Task<IActionResult> LegoList()
        {
            var legoSets = await _legoRepository.GetAllAsync();
            Console.WriteLine($"Total LEGO sets: {legoSets.Count}"); 
            return View(legoSets);
        }
         // GET: Create page
        public IActionResult Create()
        {
            return View();
        }
        // POST: POST: Handle Form Submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveNewLegoSet(Legoset legoSet)
        {
            if (ModelState.IsValid)
            {
                await _legoRepository.AddAsync(legoSet);
                return RedirectToAction(nameof(LegoList));
            }
            return View("Create",legoSet);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}