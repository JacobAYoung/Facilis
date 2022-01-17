using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherForecast.Abstraction;

namespace ASP.NETCoreWebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherService weatherService;
        private readonly ICache cache;

        public WeatherForecastController(IWeatherService weatherService, ILogger<WeatherForecastController> logger, ICache cache)
        {
            this.weatherService = weatherService;
            this.cache = cache;
            _logger = logger;
        }

        [HttpGet]
        public Task<IEnumerable<IWeatherForecast>> GetWeatherForecast()
        {
            try
            {
                const string CacheKey = "weather-forecast";
                
                return cache.GetOrCreateAsync(
                        CacheKey,
                        cacheEntry =>
                        {
                            return weatherService.ForecastAsync();
                        });
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Failed to process {nameof(GetWeatherForecast)}");
                throw new Exception($"Failed to process {nameof(GetWeatherForecast)}");
            }
        }
    }
}