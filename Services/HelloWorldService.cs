public class HelloWorldService: IHelloWorldService
{
  public string GetHelloWorld()
  {
    return "Hello World!";
  }

}

// Interfaces
public interface IHelloWorldService
{
  string GetHelloWorld();
}