using System;

namespace Shared
{
    public class Report
    {
        public Guid Id { get; set; }
        public string Locaiton { get; set; }
        //O konumda yer alan rehbere kayıtlı otel sayısı
        public int HotelsCount { get; set; }
        //O konumda yer alan rehbere kayıtlı telefon numarası sayısı
        public int PhonesCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public ReportStatus Status { get; set; }
        public enum ReportStatus
        {
            Preparing,
            Completed
        }
    }
}
