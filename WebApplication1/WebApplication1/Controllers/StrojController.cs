using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StrojController : Controller
    {
        private readonly IStrojService _strojService;

        public StrojController(IStrojService strojService)
        {
            _strojService = strojService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _strojService.GetStrojList();

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetStroj(int id)
        {
            var result = await _strojService.GetStroj(id);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddStroj([FromBody] Stroj stroj)
        {
            var result = await _strojService.CreateStroj(stroj);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStroj([FromBody] Stroj stroj)
        {
            var result = await _strojService.UpdateStroj(stroj);

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteStroj(int id)
        {
            var result = await _strojService.DeleteStroj(id);

            return Ok(result);
        }
    }
}

