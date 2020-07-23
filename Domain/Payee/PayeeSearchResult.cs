namespace Domain.Payee
{
    public class PayeeSearchResult
    {
        public string SearchTerm { get; set; }
        public Payee Payee { get; set; }
        public string Overlap { get; set; }
    }
}
