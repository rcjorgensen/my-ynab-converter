namespace Domain.Payee
{
    public class Keyword
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int PayeeId { get; set; }

        public bool Deleted { get; set; }
    }
}
