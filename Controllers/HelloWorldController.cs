using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers;

[ApiController]
[Route("api/[controller]")] 
public class HelloWorldController: ControllerBase
{
  private readonly ILogger<HelloWorldController> _logger;
  IHelloWorldService helloWorldService;
  TareasContext dbContext;
  
  //Inyeccion de dependecias
  public HelloWorldController(ILogger<HelloWorldController> logger, IHelloWorldService helloWorldService, TareasContext db)
  {
    _logger = logger;
    this.helloWorldService = helloWorldService;
    this.dbContext = db;
  }

  [HttpGet] // PAra cumplir con el estandar de swagger
  public IActionResult Get() //Metodo Get
  {
    _logger.LogInformation("Retornando Hello_World"); // Debug o console.log('parecido');
    return Ok(helloWorldService.GetHelloWorld()); // status 200
  }

  [HttpGet]
  [Route("createdb")] // nombre endpoint
  public IActionResult CreateDatabase()
  {
    // Crear DB
    this.dbContext.Database.EnsureCreated();
    return Ok("Base de datos creada");
  }

}