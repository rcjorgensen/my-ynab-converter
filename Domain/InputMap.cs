using CsvHelper.Configuration;
using System.Globalization;

namespace Domain
{
    internal class InputMap : ClassMap<Input>
    {
        public InputMap()
        {
            Map(m => m.Date)
                .Name("Dato")
                .TypeConverterOption.Format("dd-MM-yyyy");
            Map(m => m.Text)
                .Name("Tekst");
            Map(m => m.Amount)
                .Name("Beløb")
                .TypeConverterOption.CultureInfo(new CultureInfo("en-US"))
                .TypeConverterOption.NumberStyles(NumberStyles.Any);
        }
    }
}
