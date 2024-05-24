using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MODotNetCore.RestApiWithNLayer.Features.Zodiac
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZodiacController : ControllerBase
    {
        private async Task<ZodiacModel> GetDataAsync()
        {
            string jsonStr = await System.IO.File.ReadAllTextAsync("zodiac.json");
            var model = JsonConvert.DeserializeObject<ZodiacModel>(jsonStr); // Json to C#
            return model;
        }

        [HttpGet("zodiacSignsDetail")]
        public async Task<IActionResult> ZodiacSignsDetail()
        {
            var model = await GetDataAsync();
            return Ok(model.ZodiacSignsDetail);
        }

        [HttpGet("trait")]
        public async Task<IActionResult> Trait()
        {
            var model = await GetDataAsync();
            ZodiacSignsDetail[] detail = model.ZodiacSignsDetail;
            return Ok(detail[0].Traits);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ZodiacData(int id)
        {
            var model = await GetDataAsync();
            return Ok(model.ZodiacSignsDetail.FirstOrDefault(x => x.Id == id));
        }
    }
}
