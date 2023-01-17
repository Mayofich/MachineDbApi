using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KvarStatusChangeController : Controller
    {
        private readonly IKvarService _kvarService;
        public int offset = 0;

        public KvarStatusChangeController(IKvarService kvarService)
        {
            _kvarService = kvarService;
        }


        [HttpPut]
        public async Task<IActionResult> UpdateStatus([FromBody] KvarStatusChange kvarStatus)
        {
            var result = await _kvarService.UpdateStatus(kvarStatus);

            return Ok(result);
        }

    }
}

