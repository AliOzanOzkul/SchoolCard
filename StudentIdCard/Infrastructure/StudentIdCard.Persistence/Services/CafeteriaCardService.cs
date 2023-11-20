using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StudentIdCard.Application.Repositories.BaseCard;
using StudentIdCard.Application.Repositories.Card;
using StudentIdCard.Domain.Entites;
using StudentIdCard.Persistence.Context;
using StudentIdCard.Persistence.Repositories.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace StudentIdCard.Persistence.Services
{
    public class CafeteriaCardService : BackgroundService
    {

        private readonly ILogger<CafeteriaCardService> _logger;

        public CafeteriaCardService(ILogger<CafeteriaCardService> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if(DateTime.Now.Hour == 0)
                {
                    CafeteriaCardUpdate cafeteriaCardService = new();
                    cafeteriaCardService.Update();

                    _logger.LogInformation("Güncelleme yapıldı");
                }
                _logger.LogInformation($"saat: {DateTime.Now.Hour}");
                await Task.Delay(6000);
            }
        }
    }

    
    public class CafeteriaCardUpdate
    {
       
        public void Update()
        {
            StudentIdCardContext _db = new();
             var selectedCard = _db.Cards.ToList();
            foreach(var item  in selectedCard)
            {
                item.Cafeteria = 0;
                _db.Cards.Update(item);
                _db.SaveChanges();
            }
        }
       
    }


}
