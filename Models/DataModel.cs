using System.Collections.Generic;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace Grades.Models
{
    public class DataModel
    {
        public List<string> Classes { get; set; }
        public string Name { get; set; }
        public string Semester { get; set; }
    }

    public class ListConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            var list = new List<string>();
            var array = text.Split('|');

            foreach (var s in array)
            {
                list.Add(s);
            }

            return list;
        }

        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            return string.Join('|', (List<string>) value);
        }
    }

    public class DataModelMap : ClassMap<DataModel>
    {
        public DataModelMap()
        {
            Map(m => m.Name).Name("name");
            Map(m => m.Semester).Name("semester");
            Map(m => m.Classes).Name("classes").TypeConverter<ListConverter>();
        }
    }
}
