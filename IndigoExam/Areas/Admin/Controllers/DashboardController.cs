using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IndigoExam.DAL;
using IndigoExam.Models;
using IndigoExam.DTOs.CardDTOs;
using NuGet.ProjectModel;
using IndigoExam.Extensions;

namespace IndigoExam.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public DashboardController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Admin/Cards
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cards.ToListAsync());
        }

        // GET: Admin/Cards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cards == null)
            {
                return NotFound();
            }

            var card = await _context.Cards
                .FirstOrDefaultAsync(m => m.Id == id);
            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        // GET: Admin/Cards/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Cards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CardPostDto cardPostDto)
        {
            await _context.AddAsync(new Card
            {
                Title = cardPostDto.Title,
                Description = cardPostDto.Description,
                Image=cardPostDto.File.CreateFile(_env.WebRootPath,"assets/images")
            });
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Cards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            Card card = await _context.Cards.FindAsync(id);
            if (card == null)
            {
                return NotFound();
            }
            CardUpdateDto cardUpdateDto = new CardUpdateDto()
            {
                cardGetDto = new CardGetDto
                {
                    Id = card.Id,
                    Title = card.Title,
                    Description = card.Description,
                    Image = card.Image
                }
            };

            return View(cardUpdateDto);
        }

        // POST: Admin/Cards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CardUpdateDto cardUpdateDto)
        {
            Card? card = _context.Cards.Find(cardUpdateDto.cardGetDto.Id);
            card.Title = cardUpdateDto.cardPostDto.Title;
            card.Description = cardUpdateDto.cardPostDto.Description;
            if (cardUpdateDto.cardPostDto.File != null)
            {
                card.Image = cardUpdateDto.cardPostDto.File.CreateFile(_env.WebRootPath, "assets/images");
            }
            _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Cards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cards == null)
            {
                return NotFound();
            }

            var card = await _context.Cards
                .FirstOrDefaultAsync(m => m.Id == id);
            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        // POST: Admin/Cards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cards == null)
            {
                return Problem("Entity set 'AppDbContext.Cards'  is null.");
            }
            var card = await _context.Cards.FindAsync(id);
            if (card != null)
            {
                _context.Cards.Remove(card);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CardExists(int id)
        {
            return _context.Cards.Any(e => e.Id == id);
        }
    }
}
