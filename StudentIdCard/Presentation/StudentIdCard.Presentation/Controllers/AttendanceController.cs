using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentIdCard.Application.Repositories.BaseCard;
using StudentIdCard.Application.Repositories.Card;
using StudentIdCard.Application.ViewModels.BaseCard;
using StudentIdCard.Domain.Entites;
using StudentIdCard.Persistence.Context;
using StudentIdCard.Persistence.Services;

namespace StudentIdCard.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private ICardWriteRepository _cardWriteRepository;
        private ICardReadRepository _cardReadRepository;
        private IBaseCardReadRepository _baseCardReadRepository;
        private readonly StudentIdCardContext _studentIdCardContext;

        public AttendanceController(ICardWriteRepository cardWriteRepository, ICardReadRepository cardReadRepository, IBaseCardReadRepository baseCardReadRepository, StudentIdCardContext studentIdCardContext)
        {
            _cardWriteRepository = cardWriteRepository;
            _cardReadRepository = cardReadRepository;
            _baseCardReadRepository = baseCardReadRepository;
            _studentIdCardContext = studentIdCardContext;
        }
        [HttpPost("Entry")]
        public async Task<IActionResult> EntryStudent(VM_Create_BaseCard model)
        {
            BaseCard selectedCard = await _baseCardReadRepository.GetSingleAsync(data => data.CardNo == model.CardNo);
            string answer = await _cardWriteRepository.StudentsEntry(selectedCard.Id.ToString());
         
          
            return Ok(answer);
        }
        [HttpPost("Leave")]
        public async Task<IActionResult> LeaveStudent(VM_Create_BaseCard model)
        {
            BaseCard selectedCard = await _baseCardReadRepository.GetSingleAsync(data => data.CardNo == model.CardNo);
            string answer = await _cardWriteRepository.StudentsLeaving(selectedCard.Id.ToString());
            return Ok(answer);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAttendance()
        {
            var list = _studentIdCardContext.Attendances
    .Select(a => new
    {
       
       
        CardName = a.Card.Name,
        CardSurname = a.Card.Surname,
        Entry = a.EntranceTime,
        Leave = a.LeavingTime
     
    })
    .ToList();
            return Ok(list);
        }
    }
}
