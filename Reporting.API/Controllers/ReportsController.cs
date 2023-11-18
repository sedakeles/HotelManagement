using Hotel.API.Models;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reporting.API.Consumers;
using Reporting.API.DTOs;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reporting.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly HotelEventConsumer _hotelEventConsumer;


        public ReportsController(AppDbContext context, HotelEventConsumer hotelEventConsumer)
        {
            _context = context;
            _hotelEventConsumer = hotelEventConsumer;
        }
        //konuma göre otel sayısı
        [HttpGet("GetHotelReportByLocationList")]
        public async Task<List<ReportForLocaitonDto>> GetHotelReportByLocation()
        {
            var report = new List<ReportForLocaitonDto>();
            report = await _hotelEventConsumer.HotelReportByLocationUpdate();
            return report;

        }

        //Sistemin oluşturduğu bir raporun detay bilgilerini getiren metod

        [HttpGet("GetHotelReportDetailList")]
        public async Task<List<ReportDetailDto>> GetHotelReportDetail()
        {
            var report = new List<ReportDetailDto>();
            report = await _hotelEventConsumer.HotelReporDetailtUpdate();
            return report;

        }
    }
}
