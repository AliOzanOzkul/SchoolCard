using Microsoft.EntityFrameworkCore;
using StudentIdCard.Application.Repositories.BaseCard;
using StudentIdCard.Application.Repositories.Card;
using StudentIdCard.Domain.Entites;
using StudentIdCard.Persistence.Context;
using StudentIdCard.Persistence.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentIdCard.Persistence.Repositories.Card
{
    public class CardWriteRepository : WriteRepository<StudentIdCard.Domain.Entites.Card>, ICardWriteRepository
    {
        private readonly ICardReadRepository _cardReadRepository;
        private readonly StudentIdCardContext _db;
        private readonly IBaseCardReadRepository _baseCardReadRepository;
        public CardWriteRepository(StudentIdCardContext context, ICardReadRepository cardReadRepository, StudentIdCardContext db, IBaseCardReadRepository baseCardReadRepository) : base(context)
        {
            _cardReadRepository = cardReadRepository;
            _db = db;
            _baseCardReadRepository = baseCardReadRepository;
        }

        public async Task<string> CafeteriaRight(string id)
        {
            StudentIdCard.Domain.Entites.BaseCard baseCard = await _baseCardReadRepository.GetSingleAsync(data => data.CardNo == id);
            StudentIdCard.Domain.Entites.Card card1 = await _cardReadRepository.GetSingleAsync(data => data.BaseCardId == baseCard.Id);
            StudentIdCard.Domain.Entites.Card card = await _cardReadRepository.GetByIdAsync(card1.Id.ToString());
            string answer;
            if (card == null)
            {
                answer = "Böyle bir kart bulunamadı";
                
            }
            else
            {
                if(card.Cafeteria < 3)
                {
                    answer = "İşlem Başarılı";
                    card.Cafeteria += 1;
                    await SaveAsync();
                }
                else
                {
                    answer = "Yemek Hakkınız dolmuştur";
                }
            }
            return answer;
        }
        public async Task<string> StudentsEntry(string id)
        {
            StudentIdCard.Domain.Entites.Card card = await _cardReadRepository.GetSingleAsync(data => data.BaseCardId == Guid.Parse(id));
            string answer;
            if (card == null)
            {
                answer = "Böyle bir kart bulunamadı";

            }
            else
            {
                card.Attendances.Add(new() { EntranceTime = DateTime.Now});
                await SaveAsync();
                SmsService sms = new();
                StudentIdCard.Domain.Entites.Card selectedStudent = await _cardReadRepository.GetByIdAsync(id);
                //sms.SendSms(selectedStudent.ParentPhoneNumber.ToString(), $"Öğrenciniz {DateTime.Now} tarihinde giriş sağlamıştır ");
                answer = "Giriş Başarılı";
            }
            return answer;
        }

        public async Task<string> StudentsLeaving(string id)
        {
            StudentIdCard.Domain.Entites.Card card = await _cardReadRepository.GetSingleAsync(data => data.BaseCardId == Guid.Parse(id));
            string answer;
            if (card == null)
            {
                answer = "Böyle bir kart bulunamadı";

            }
            else
            {
                StudentIdCard.Domain.Entites.Card card1 = await _cardReadRepository.GetSingleAsync(data => data.BaseCardId == Guid.Parse(id));
                var attentionList = _db.Attendances.Include(c => c.Card).Where(c => c.Card.Id == card1.Id).ToList();

                attentionList[attentionList.Count-1].LeavingTime = DateTime.Now;
                SmsService sms = new();
                StudentIdCard.Domain.Entites.Card selectedStudent = await _cardReadRepository.GetByIdAsync(id);
                //sms.SendSms(selectedStudent.ParentPhoneNumber.ToString(), $"Öğrenciniz {DateTime.Now} tarihinde çıkış sağlamıştır ");
                await SaveAsync();
                answer = "Çıkış Başarılı";
            }
            return answer;
        }
    }
}
