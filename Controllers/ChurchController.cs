using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sacremap.Services.ChurchService;
using sacremap_web_api.Models;

namespace sacremap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChurchController : ControllerBase
    {
        private readonly IChurchService churchService;


        public ChurchController(IChurchService churchService)
        {
            this.churchService = churchService;
        }
        [HttpGet("s/{search}")]
        public async Task<ActionResult<List<Church>>> SearchByCityName(string search)
        {
            if (await churchService.SearchByCities(search) is not null)
            {
                return Ok(await churchService.SearchByCities(search));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<Church>>> GetAll()
        {
            return Ok(await churchService.GetChurchList());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Church>> GetOne(int id)
        {
            if (await churchService.GetChurch(id) is not null)
            {
                return Ok(await churchService.GetChurch(id));
            }
            else return NotFound();
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("admin/{id}")]
        public async Task<ActionResult<Church>> Update(int id, Church newChurch)
        {
            if (await churchService.EditChurch(id, newChurch) is not null)
            {
                return Ok();
            }
            else return BadRequest();

        }
        [Authorize(Roles = "Admin")]
        [HttpPost("admin/create")]
        public async Task<ActionResult<Church>> Create(Church newChurch)
        {
            if (newChurch is not null)
            {
                return Ok(await churchService.AddChurch(newChurch));
            }
            else return BadRequest();
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("admin/{id}")]
        public async Task<ActionResult<Church>> Delete(int id)
        {
            if (await churchService.DeleteChurch(id) is not null)
            {
                return Ok();
            }
            else return NotFound();

        }

    }
}
