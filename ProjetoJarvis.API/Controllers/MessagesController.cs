using Microsoft.AspNetCore.Mvc;

namespace ProjetoJarvis.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Jarvis funcionando!");
        }
    }
}