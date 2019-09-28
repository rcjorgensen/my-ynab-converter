using System;

namespace Domain
{
    public class Output
    {
        public DateTime Date { get; set; }
        public string Payee { get; set; }
        public string Category { get; set; }
        public string Memo { get; set; }
        public decimal? Outflow { get; set; }
        public decimal? Inflow { get; set; }
    }
}
