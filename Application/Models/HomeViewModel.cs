using Domain;
using System.Collections.Generic;

namespace Application.Models
{
    public class HomeViewModel
    {
        public List<Input> InputRecords { get; set; } = new List<Input>();
        public List<Output> OutputRecords { get; set; } = new List<Output>();
        public string Output { get; set; }
    }
}
