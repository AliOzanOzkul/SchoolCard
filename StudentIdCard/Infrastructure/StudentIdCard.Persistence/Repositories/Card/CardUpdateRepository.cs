using StudentIdCard.Application.Repositories.Card;
using StudentIdCard.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentIdCard.Persistence.Repositories.Card
{
    public class CardUpdateRepository : ICardUpdateRepository
    {
        private StudentIdCardContext _context;

        public CardUpdateRepository(StudentIdCardContext context)
        {
            _context = context;
        }

        public void UpdateCard()
        {
            var getList = _context.Cards.ToList();
            foreach (var card in getList)
            {
                card.Cafeteria = 0;
                _context.Cards.Update(card);
                _context.SaveChanges();
            }
        }
    }
}
