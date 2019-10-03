using DataAccess;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Application.RazorPages.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IPayeeRepository payeeRepository;

        public List<Input> InputRecords { get; set; } = new List<Input>();
        public List<Output> OutputRecords { get; set; } = new List<Output>();
        public string Output { get; set; }

        public IFormFile FormFile { get; set; }

        public IndexModel(IPayeeRepository payeeRepository)
        {
            this.payeeRepository = payeeRepository;
        }

        public void OnGet()
        {

        }

        public void OnPost()
        {
            var payees = payeeRepository.GetAll();

            var input = new StringBuilder();
            using (var reader = new StreamReader(FormFile.OpenReadStream(), Encoding.GetEncoding("ISO-8859-1")))
            {
                while (reader.Peek() >= 0)
                {
                    input.AppendLine(reader.ReadLine());
                }
            }

            var inputRecords = MyConverter.GetInputRecords(input.ToString());
            var outputRecords = new List<Output>();
            foreach (var record in inputRecords)
            {
                outputRecords.Add(MyConverter.Convert(record, payees));
            }
            
            InputRecords.AddRange(inputRecords);
            OutputRecords.AddRange(outputRecords);
            Output = MyConverter.GetOutputString(outputRecords);
        }
    }
}
