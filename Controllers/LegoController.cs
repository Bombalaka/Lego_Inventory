using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Lego_Inventory.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Lego_Inventory.Models;

namespace Lego_Inventory.Controllers
{
    
    public class LegoController : Controller
    {
        private readonly ILegoRepository _legoRepository;

        public LegoController(ILegoRepository legoRespository)
        {
            _legoRepository = legoRespository;
        }
        // GET: /Lego/
        public async Task<IActionResult> Index()
        {
            var legoSets = await _legoRepository.GetAllAsync();
            return View(legoSets);
        }
        // GET: /Lego/Create
        public IActionResult Create()
        {
            return View();
        }
        // POST: /Lego/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Legoset legoset)
        {
            if (ModelState.IsValid)
            {
                await _legoRepository.AddAsync(legoset);
                return RedirectToAction(nameof(Index));
            }
            return View(legoset);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}