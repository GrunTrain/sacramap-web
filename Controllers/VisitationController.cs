using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sacremap.Models;
using sacremap.Services.VisitationService;
using sacremap_web_api.Models;

namespace sacremap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VisitationController : ControllerBase
    {
        private readonly IVisitationService _visitationService;
        private readonly Microsoft.AspNetCore.Identity.UserManager<User> _userManager;

        public VisitationController(IVisitationService visitationService, Microsoft.AspNetCore.Identity.UserManager<User> userManager)
        {
            _visitationService = visitationService;
            _userManager = userManager;
        }

        private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);


        [HttpGet("user/visitations")]
        public async Task<ActionResult<List<Visitation>>> GetAll()
        {
            return Ok(await _visitationService.GetVisitationsListForUser(User.Identity.GetUserId()));
        }
        [HttpGet("user/visitations/{id}")]
        public async Task<ActionResult<List<Visitation>>> Get(int id)
        {
            if (_visitationService.GetVisitation(id) is not null)
            {
                return Ok(await _visitationService.GetVisitation(id));
            }
            else return NotFound();
        }
        [HttpPost("{id}/user/add")]
        public async Task<ActionResult<Visitation>> AddVisitation(int id)
        {
            /*            if (await _visitationService.AddVisitation(User.Identity.GetUserId(), id) is not null)
                        {
                            return Ok();
                        }
                        else return BadRequest();*/
            var user = await GetCurrentUserAsync();
            return Ok(await _visitationService.AddVisitation(user.Id, id));

        }

    }
}
