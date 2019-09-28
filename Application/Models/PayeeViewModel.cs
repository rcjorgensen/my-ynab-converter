using System.Collections.Generic;

namespace Application.Models
{
    public class PayeeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Keywords { get; set; }
    }
}
