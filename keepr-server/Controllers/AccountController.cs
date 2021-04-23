using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using keeprapp.Services;
using keeprapp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using CodeWorks.Auth0Provider;

namespace keeprapp.Controllers
{

    [Route("[controller]")]
    [ApiController]

    public class AccountController : ControllerBase
    {
        private readonly ProfilesService _ps;
        private readonly KeeprsService _ks;

        public AccountController(ProfilesService ps, KeeprsService ks)
        {
            _ps = ps;
            _ks = ks;

        }

        [HttpGet]
        public async Task<ActionResult<Profile>> Get()
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_ps.GetOrCreateProfile(userInfo));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                throw;
            }
        }
        [Authorize]
        [HttpGet("keeps")]
        public async Task<ActionResult<IEnumerable<VaultKeepViewModel>>> GetKeepsAsync()
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_ks.GetByAccountId(userInfo.Id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}