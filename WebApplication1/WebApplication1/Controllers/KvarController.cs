using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KvarController : Controller
    {
        private readonly IKvarService _kvarService;

        public KvarController(IKvarService kvarService)
        {
            _kvarService = kvarService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _kvarService.GetKvarList();

            return Ok(result);
        }

        [HttpGet("{id_stroja:int}")]
        public async Task<IActionResult> GetKvar(int id_stroja)
        {
            var result = await _kvarService.GetKvar(id_stroja);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddKvar([FromBody] Kvar kvar)
        {
            var result = await _kvarService.CreateKvar(kvar);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateKvar([FromBody] Kvar kvar)
        {
            var result = await _kvarService.UpdateKvar(kvar);

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteKvar(int id)
        {
            var result = await _kvarService.DeleteKvar(id);

            return Ok(result);
        }
    }
}

