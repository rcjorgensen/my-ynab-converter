using CsvHelper;
using Domain.Payee;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Domain
{
    public static class MyConverter
    {
        public static List<Input> GetInputRecords(string input)
        {
            using (var inputReader = new StringReader(input))
            using (var csvReader = new CsvReader(inputReader))
            {
                csvReader.Configuration.Delimiter = ";";
                csvReader.Configuration.HasHeaderRecord = true;
                csvReader.Configuration.RegisterClassMap<InputMap>();
                return csvReader.GetRecords<Input>().ToList();
            }
        }

        public static Output Convert(Input input, List<Payee.Payee> payees)
        {
            var payeeSearchResult = PayeeFinder.Find(input.Text, payees).FirstOrDefault();
            return new Output
            {
                Date = input.Date,
                PayeeBefore = input.Text,
                PayeeAfter = payeeSearchResult.Payee?.Name ?? string.Empty,
                Overlap = payeeSearchResult.Overlap,
                Category = "",
                Memo = "",
                Inflow = input.Amount >= 0 ? input.Amount : new decimal?(),
                Outflow = input.Amount < 0 ? -input.Amount : new decimal?()
            };
        }

        public static string GetOutputString(List<Output> outputRecords)
        {
            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream))
            using (var csvOutput = new CsvWriter(writer))
            using (var outputReader = new StreamReader(stream))
            {
                csvOutput.Configuration.Delimiter = ",";
                csvOutput.Configuration.HasHeaderRecord = true;
                csvOutput.Configuration.RegisterClassMap<OutputMap>();
                csvOutput.WriteRecords(outputRecords);
                writer.Flush();
                stream.Position = 0;

                return outputReader.ReadToEnd();
            }
        }
    }
}
