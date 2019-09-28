using Application.Models;
using DataAccess;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Application.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPayeeRepository repository;

        public HomeController(IPayeeRepository repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            return View(new HomeViewModel());
        }

        [HttpPost]
        public IActionResult Index(IFormFile file)
        {
            var payees = repository.GetAll();

            var input = new StringBuilder();
            using (var reader = new StreamReader(file.OpenReadStream(), Encoding.GetEncoding("ISO-8859-1")))
            {
                while (reader.Peek() >= 0)
                    input.AppendLine(reader.ReadLine());
            }

            var inputRecords = MyConverter.GetInputRecords(input.ToString());
            var outputRecords = new List<Output>();
            foreach (var record in inputRecords)
            {
                outputRecords.Add(MyConverter.Convert(record, payees));
            }
            var output = MyConverter.GetOutputString(outputRecords);

            var model = new HomeViewModel();
            model.InputRecords.AddRange(inputRecords);
            model.OutputRecords.AddRange(outputRecords);
            model.Output = output;
            return View(model);
        }
    }
}
