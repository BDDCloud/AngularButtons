using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AngularSample.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private static Dictionary<string, string> colors = new Dictionary<string, string>()
        {
            {"#FFFFFF", "White"},
            {"#C0C0C0", "Silver"},
            {"#808080", "Gray"},
            {"#000000", "Black"},
            {"#FF0000", "Red"},
            {"#800000", "Maroon"},
            {"#FFFF00", "Yellow"},
            {"#808000", "Olive"},
            {"#00FF00", "Lime"},
            {"#008000", "Green"},
            {"#00FFFF", "Aqua"},
            {"#008080", "Teal"},
            {"#0000FF", "Blue"},
            {"#000080", "Name"},
            {"#FF00FF", "Fuchsia"},
            {"#800080", "Purple"},
        };



        [HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }

        [HttpGet("[action]")]
        public IEnumerable<CustomButton> CustomButtons()
        {
            var rng = new Random();
            

            return Enumerable.Range(1, 15).Select(index => new CustomButton
            {
                Id = index.ToString(),
                Text = new String(Enumerable.Range(1, 10).Select(index2 => (char)('A' + rng.Next(0, 25))).ToArray()),
                Color = $"{colors.Keys.Select((k,v) => k).ToArray()[rng.Next(0, colors.Count-1)]}"
            });
        }

        [HttpPost("[action]")]
        public ColorClass LookupColorName()
        {
            StreamReader reader = new StreamReader(HttpContext.Request.Body);
            string color = reader.ReadToEnd();

            return new ColorClass() {ColorName = colors[color]};
        }

        public class ColorClass
        {
            public string ColorName { get; set; }
        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(TemperatureC / 0.5556);
                }
            }
        }

        public class CustomButton
        {
            public string Id { get; set; }
            public string Color { get; set; }
            public string Text { get; set; }
        }
    }
}
