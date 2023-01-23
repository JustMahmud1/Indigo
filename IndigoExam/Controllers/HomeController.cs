using IndigoExam.DAL;
using IndigoExam.DTOs;
using IndigoExam.DTOs.CardDTOs;
using IndigoExam.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace IndigoExam.Controllers
{
    public class HomeController : Controller
    {
        public readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IQueryable<Card> cards = _context.Cards;
            GetAllDto<CardGetDto> getAllDto = new();
            getAllDto.Items = cards.Select(x => new CardGetDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Image = x.Image,
            }).ToList();
            return View(getAllDto.Items);
        }

    }
}