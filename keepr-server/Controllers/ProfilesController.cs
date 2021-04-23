
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using keeprapp.Models;
using keeprapp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace keeprapp.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly ProfilesService _pservice;
        private readonly KeeprsService _ks;
        private readonly VaultsService _vs;

        public ProfilesController(ProfilesService pservice, KeeprsService ks, VaultsService vs)
        {
            _pservice = pservice;
            _ks = ks;
            _vs = vs;
        }
        [HttpGet("{id}")]

        public ActionResult<ProfilesController> GetByProfileId(string id)
        {
            try
            {
                return Ok(_pservice.GetProfileById(id));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }
        [HttpGet("{id}/keeps")]
        public ActionResult<IEnumerable<VaultKeepViewModel>> GetKeepsByProfileId(string id)
        {
            try
            {
                return Ok(_ks.GetKeepsByProfileId(id));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        // [Authorize]
        [HttpGet("{profileId}/vaults")]
        public async Task<ActionResult<IEnumerable<Vault>>> GetVaultsByProfileIdAsync(string profileId)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_vs.GetByProfileId(profileId));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

    }
}