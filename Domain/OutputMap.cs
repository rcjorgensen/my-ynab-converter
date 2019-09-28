using CsvHelper.Configuration;
using System.Globalization;

namespace Domain
{
    internal class OutputMap : ClassMap<Output>
    {
        public OutputMap()
        {
            Map(m => m.Date)
                .Index(0)
                .Name("Date")
                .TypeConverterOption.Format("yyyy-MM-dd");
            Map(m => m.Payee)
                .Index(1)
                .Name("Payee");
            Map(m => m.Category)
                .Index(2)
                .Name("Category");
            Map(m => m.Memo)
                .Index(3)
                .Name("Memo");
            Map(m => m.Outflow)
                .Index(4)
                .Name("Outflow")
                .TypeConverterOption.CultureInfo(new CultureInfo("en-US"))
                .TypeConverterOption.NumberStyles(NumberStyles.Any);
            Map(m => m.Inflow)
                .Index(5)
                .Name("Inflow")
                .TypeConverterOption.CultureInfo(new CultureInfo("en-US"))
                .TypeConverterOption.NumberStyles(NumberStyles.Any);
        }
    }
}
