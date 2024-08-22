using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Globalization;
using WebApplication2.Models;
namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserSettingController : ControllerBase
    {
        private readonly ILogger<UserSettingController> _logger;

        public UserSettingController(ILogger<UserSettingController> logger)
        {
            _logger = logger;
        }

        [HttpGet("CheckSettings")]
        public bool CheckSettings(string input, int setting)
        {
            bool output = false;
            return output = input.ToString().Substring(setting, 1) == "1";
        }
        [HttpGet("ReadCSV")]
        public List<UserSetting> ReadCSV()
        {
            string filePath = @"c:\temp\test.csv";
            List<UserSetting> records = new List<UserSetting>();
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, config))
            {
                var result = csv.GetRecords<UserSetting>();
                foreach(var record in result)
                {
                    records.Add(record);
                }
            }
            return records;
        }
        [HttpGet("WriteCSV")]
        public void WriteCSV(string addSetting)
        {
            var newSettings = new List<UserSetting>
            {
                new UserSetting { user_setting = addSetting },               
            };
            // Path to the CSV file
            string filePath = @"c:\temp\test.csv";
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };
            // Open the file for writing
            using (var writer = new StreamWriter(filePath,true))
            using (var csv = new CsvWriter(writer, config))
            {
                // Write the data to the CSV file
                csv.WriteRecords(newSettings);
            }
        }



    }
}