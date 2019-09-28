using System.Collections.Generic;

namespace Domain.Payee
{
    public class Payee
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Keyword> Keywords { get; set; } = new List<Keyword>();
    }
}
