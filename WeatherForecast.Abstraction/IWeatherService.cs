using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeatherForecast.Abstraction
{
    public interface IWeatherService
    {
        Task<IEnumerable<IWeatherForecast>> ForecastAsync();
    }
}