using Microsoft.AspNetCore.Mvc;
using StudentIdCard.Application.Repositories.Card;
using StudentIdCard.Application.Repositories.User;
using StudentIdCard.Application.ViewModels.User;
using StudentIdCard.Domain.Entites;

namespace StudentIdCard.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserWriteRepository _userWriteRepository;
        private readonly IUserReadRepository _userReadRepository;
        private readonly ICardReadRepository _cardReadRepository;

        public UserController(IUserWriteRepository userWriteRepository, IUserReadRepository userReadRepository, ICardReadRepository cardReadRepository)
        {
            _userWriteRepository = userWriteRepository;
            _userReadRepository = userReadRepository;
            _cardReadRepository = cardReadRepository;
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(VM_Create_User model)
        {
            var control = await _userReadRepository.GetSingleAsync(data => data.UserName == model.UserName);
            var control2 = await _cardReadRepository.GetSingleAsync(data => data.ParentUserName == model.UserName);
            if (control == null && control2 == null)
            {
                User user = new()
                {
                    UserName = model.UserName,
                    Password = model.Password
                };
                await _userWriteRepository.AddAsync(user);
                await _userWriteRepository.SaveAsync();
                return Ok("Kayıt Oluşturuldu");
            }
            else
            {
                return Ok("Bu kullanıcı adı daha önceden alındı");
            }
        }
        [HttpPost("Login")]
        public async Task<object> Login(VM_Create_User model)
        {
            User control = await _userReadRepository.GetSingleAsync(data => data.UserName == model.UserName);
            Card control2 = await _cardReadRepository.GetSingleAsync(data => data.ParentUserName == model.UserName);
            if (control != null)
            {
                User selectedUser = await _userReadRepository.GetSingleAsync(data => data.Password == model.Password);
                if (selectedUser != null)
                {
                    var data = new { Id = selectedUser.Id.ToString(), Role = "Admin" };
                    return Ok(data);
                }
                else
                {
                    return "Böyle bir kullanıcı bulunamadı";
                }
            }
            else if (control2 != null)
            {
                Card selectedUser2 = await _cardReadRepository.GetSingleAsync(data => data.ParentPassword == model.Password && data.ParentUserName == model.UserName);
                if (selectedUser2 != null)
                {
                    var data = new
                    {
                        Id = selectedUser2.Id.ToString(),
                        Role = "Veli"
                    };
                    return Ok(data);
                }
                else
                {
                    return "Böyle bir kullanıcı bulunamadı";
                }
            }
            else
            {
                return "Böyle bir kullanıcı bulunamadı";
            }

        }
    }
}
