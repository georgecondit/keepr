using System;
using System.Threading.Tasks;
using keeprapp.Models;
using keeprapp.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keeprapp.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class VaultKeepsController : ControllerBase
    {
        private readonly VaultKeepService _vkserve;

        public VaultKeepsController(VaultKeepService vkserve)
        {
            _vkserve = vkserve;
        }




        [Authorize]
        public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep newVK)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                newVK.CreatorId = userInfo.Id;
                return Ok(_vkserve.Create(newVK));
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<VaultKeep>> Delete(int id, string ProfileId)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_vkserve.Delete(id, userInfo.Id));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}