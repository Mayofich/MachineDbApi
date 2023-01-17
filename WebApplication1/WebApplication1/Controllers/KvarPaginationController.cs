using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KvarPaginationController : Controller
    {
        private readonly IKvarService _kvarService;
        public int offset = 0;

        public KvarPaginationController(IKvarService kvarService)
        {
            _kvarService = kvarService;
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePagination([FromBody] KvarPagination kvarPagination)
        {
            var result = await _kvarService.UpdatePagination(kvarPagination);

            return Ok(result);
        }

    }
}

