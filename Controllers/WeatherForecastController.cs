using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers;

[ApiController]
[Route("api/[controller]")] // Generar ruta
public class WeatherForecastController : ControllerBase
{
  private static readonly string[] Summaries = new[]
  {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

  private readonly ILogger<WeatherForecastController> _logger;

  private static List<WeatherForecast> ListWeatherForecast = new List<WeatherForecast>();

  public WeatherForecastController(ILogger<WeatherForecastController> logger)
  {
    _logger = logger;

    if (ListWeatherForecast == null || !ListWeatherForecast.Any()) // Any() Sino tiene ningun dato en la lista
    {
      ListWeatherForecast = Enumerable.Range(1, 5).Select(index => new WeatherForecast
      {
        Date = DateTime.Now.AddDays(index),
        TemperatureC = Random.Shared.Next(-20, 55),
        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
      }).ToList();
    }

  }

  [HttpGet(Name = "GetWeatherForecast")]
  // COMENTAMOS PARA LLEVAR EL ESTANDAR CON SWAGGER UI
  // [Route("Get/weatherforecast")] // Agregar ruta de ruta en ruta
  // [Route("Get/weatherforecast2")] // https://localhost:7294/api/weatherforecast/get/weatherforecast2
  // [Route("[action]")] // Permite utilizar el nombre delmetodo Get => https://localhost:7294/api/weatherforecast/get
  public IEnumerable<WeatherForecast> Get()
  {
    _logger.LogInformation("Retornando la lista de watherforeacast"); // Debug o console.log('parecido');
    return ListWeatherForecast;
  }

  [HttpPost]
  public IActionResult Post(WeatherForecast weatherForecast)
  {
    ListWeatherForecast.Add(weatherForecast);

    return Ok();
  }

  [HttpDelete("{index}")]
  public IActionResult Delete(int index)
  {
    ListWeatherForecast.RemoveAt(index);

    return Ok();
  }
}
