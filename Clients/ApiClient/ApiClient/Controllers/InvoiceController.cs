using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ApiClient.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvoiceController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok();
        }
    }
}
