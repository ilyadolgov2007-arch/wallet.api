using Microsoft.AspNetCore.Mvc;
 
namespace Wallet.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloController : ControllerBase
    {
        [HttpGet]
        public IActionResult SayHello()
        {
            return Ok("привет от кошелька!");
        }

        [HttpGet("{name}")]
        public IActionResult SayHelloTo(string name)
        {
            if(name.Length < 2)
            {
                return BadRequest($"Имя слишком короткое получено {name}") ;
            }
            return Ok($"Привет, {name}!");
        }

    }
}
