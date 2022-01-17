using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecast.Abstraction;

namespace WeatherForecast
{
    public class WeatherService : IWeatherService
    {
        private static readonly string[] Summaries =
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public async Task<IEnumerable<IWeatherForecast>> ForecastAsync()
        {
            return await WeatherForecastSimulation();;
        }

        private async Task<IEnumerable<IWeatherForecast>> WeatherForecastSimulation()
        {
            await Task.Delay(4000);
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index =>
                    new WeatherForecast
                    {
                        Date = DateTime.Now.AddDays(index),
                        TemperatureC = rng.Next(-20, 55),
                        Summary = Summaries[rng.Next(Summaries.Length)]
                    })
                .ToArray();
        }
    }
}