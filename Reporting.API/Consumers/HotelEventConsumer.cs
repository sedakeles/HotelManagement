using Hotel.API.Models;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Reporting.API.DTOs;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reporting.API.Consumers
{
    public class HotelEventConsumer : IConsumer<HotelCreatedEvent>,IConsumer<HotelDeletedEvent>
    {
        private readonly AppDbContext _context;
        private ILogger<HotelEventConsumer> _logger;

        public HotelEventConsumer(AppDbContext context, ILogger<HotelEventConsumer> logger)
        {
            _context = context;
            _logger = logger;

        }
        public async Task Consume(ConsumeContext<HotelCreatedEvent> context)
        {
            var hotelCreatedEvent = context.Message;
            await HotelReporDetailtUpdate();
            await  HotelReportByLocationUpdate();
        }
        public async Task Consume(ConsumeContext<HotelDeletedEvent> context)
        {
            var hotelDeletedEvent = context.Message;
            await HotelReporDetailtUpdate();
            await HotelReportByLocationUpdate();
        }
        public async Task<List<ReportForLocaitonDto>> HotelReportByLocationUpdate()
        {

            var hotels = await _context.Hotels
          .Include(o => o.ContactInformations)
          .ToListAsync();

            var response = hotels
           .SelectMany(o => o.ContactInformations.Select(ContactInfo => new { ContactInformations = ContactInfo, Otel = o }))
           .GroupBy(x => x.ContactInformations.Location)
           .Select(group => new ReportForLocaitonDto
           {
               Locaiton = group.Key,
               HotelsCount = group.Count(),
               Status = ReportForLocaitonDto.ReportStatus.Completed

           }).ToList();

            return response;

        }
        public async Task<List<ReportDetailDto>> HotelReporDetailtUpdate()
        {

            var hotels = await _context.Hotels
                      .Include(o => o.ContactInformations)
                      .ToListAsync();

            var reportList = hotels
           .Select(o => new ReportDetailDto
           {
               HotelId = o.Id,
               LocaitonsCount = o.ContactInformations?.Select(ib => ib.Location).Distinct().Count() ?? 0,
               PhonesCount = o.ContactInformations?.Where(ib => !string.IsNullOrEmpty(ib.PhoneNumber)).Count() ?? 0,
               ReportDate = DateTime.Now,
               Status = ReportDetailDto.ReportStatus.Completed,

           })
           .ToList();

            return reportList;

        }

      
    }
}
