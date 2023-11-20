using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentIdCard.Application.Repositories.Card;
using StudentIdCard.Domain.Entites;

namespace StudentIdCard.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CafeteriaController : ControllerBase
    {
        private ICardWriteRepository _cardWriteRepository;
        private readonly ICardReadRepository _cardReadRepository;

        public CafeteriaController(ICardWriteRepository cardWriteRepository, ICardReadRepository cardReadRepository)
        {
            _cardWriteRepository = cardWriteRepository;
            _cardReadRepository = cardReadRepository;
        }
        [HttpPut]
        public async Task<IActionResult> EntiryCafeteria(string id)
        {
            string answer = await _cardWriteRepository.CafeteriaRight(id);
            return Ok(answer);
        }
        [HttpGet("GetHasNotEaten")]
        public async Task<IActionResult> GetHasNotEaten()
        {
            var selectedList = _cardReadRepository.GetAll().ToList();
            List<Card> cards = new List<Card>();    
            foreach (var item in selectedList)
            {
                if (item.Cafeteria == 0)
                {
                    cards.Add(item);
                }
            }
            return Ok(cards);
        }

        [HttpGet("GetHasEaten")]
        public async Task<IActionResult> GetHasEaten()
        {
            var selectedList = _cardReadRepository.GetAll().ToList();
            List<Card> cards = new List<Card>();
            foreach (var item in selectedList)
            {
                if (item.Cafeteria > 0)
                {
                    cards.Add(item);
                }
            }
            return Ok(cards);
        }
    }
}
