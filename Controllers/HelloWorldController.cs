using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers;

[ApiController]
[Route("api/[controller]")] 
public class HelloWorldController: ControllerBase
{
  IHelloWorldService helloWorldService;
  
  //Inyeccion de dependecias
  public HelloWorldController(IHelloWorldService helloWorldService)
  {
    this.helloWorldService = helloWorldService;
  }

  public IActionResult Get() //Metodo Get
  {
    return Ok(helloWorldService.GetHelloWorld()); // status 200
  }

}