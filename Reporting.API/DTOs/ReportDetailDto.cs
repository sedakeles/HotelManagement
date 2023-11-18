using System;

namespace Reporting.API.DTOs
{
    public class ReportDetailDto
    {
        public Guid HotelId { get; set; }
        public int LocaitonsCount { get; set; }
        public int PhonesCount { get; set; }
        public DateTime ReportDate { get; set; }
        public ReportStatus Status { get; set; }
        public enum ReportStatus
        {
            Preparing,
            Completed
        }
    }
}
