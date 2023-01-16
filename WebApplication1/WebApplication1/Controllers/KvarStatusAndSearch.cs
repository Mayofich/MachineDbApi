using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KvarStatusAndSearch : Controller
    {
        private readonly IKvarService _kvarService;
        public int offset = 0;

        public KvarStatusAndSearch(IKvarService kvarService)
        {
            _kvarService = kvarService;
        }

        [HttpGet("{limit:int}")]
        public async Task<IActionResult> Get(int limit)
        {
            var result = await _kvarService.GetKvarPagination(limit, offset);
            offset += limit;
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStatus([FromBody] KvarStatusChange kvarStatus)
        {
            var result = await _kvarService.UpdateStatus(kvarStatus);

            return Ok(result);
        }

    }
}

