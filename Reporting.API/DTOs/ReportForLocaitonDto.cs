using System;

namespace Reporting.API.DTOs
{
    public class ReportForLocaitonDto
    {
        
        public string Locaiton { get; set; }
        public int HotelsCount { get; set; }
        public ReportStatus Status { get; set; }
        public enum ReportStatus
        {
           Preparing,
           Completed
        }
    }
}
