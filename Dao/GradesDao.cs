using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using Grades.Models;
using Grades.Services;
using Microsoft.Extensions.Logging;
using Spectre.Console;

namespace Grades.Dao
{
    internal class GradesDao : IGradesDao
    {
        private readonly ILogger<MenuService> _logger;
        public const string filePath = "Files/registration.txt";
        private readonly TextInfo _textInfo;

        private List<DataModel> _fileRecords;

        public GradesDao(ILogger<MenuService> logger)
        {
            _logger = logger;
            _textInfo = new CultureInfo("en-US", false).TextInfo;
        }

        public void Write(DataModel dataModelInput)
        {
            if (dataModelInput == null) return;
            var records = new List<DataModel> {dataModelInput};

            // Append to the file.
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                // Don't write the header again.
                HasHeaderRecord = false
            };

            using var writer = new StreamWriter(filePath, true);

            _logger.LogInformation("Writing data file");
            using (var csv = new CsvWriter(writer, config))
            {
                csv.Context.RegisterClassMap<DataModelMap>();
                csv.WriteRecords(records);
            }

            writer.Close();
        }

        public void Read()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header.ToLower()
            };

            using var reader = new StreamReader(filePath);

            _logger.LogInformation("Reading data file");
            using (var csv = new CsvReader(reader, config))
            {
                csv.Context.RegisterClassMap<DataModelMap>();
                _fileRecords = csv.GetRecords<DataModel>().ToList();
            }

            reader.Close();
        }

        public void Display()
        {
            foreach (var r in _fileRecords)
            {
                var table = new Table
                {
                    Title = new TableTitle($"Registration for {_textInfo.ToTitleCase(r.Name)}"),
                    Border = TableBorder.Rounded
                };
                table.AddColumn("Semester");
                table.AddColumn(new TableColumn("Classes").LeftAligned());

                var semester = r.Semester;
                foreach (var className in r.Classes)
                {
                    table.AddRow(semester, className);
                    semester = "";
                }

                var panel = new Panel(table)
                {
                    Border = BoxBorder.Heavy
                };

                AnsiConsole.Write(panel);
            }
        }
    }
}