using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

using Microsoft.Extensions.Configuration;

namespace Sudoku_WebService.Controllers
{
    /// <summary>
    /// The purpose for this controller is to get a unique id for each user.
    /// 
    /// Consideration will be required for the time between Get and Put.
    /// 
    /// This is by no means authentication, as this restricts no one, but it will 
    /// allow the services to demonstate authorization while still allowing new users.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private const string AllowedVerbs = "Get, Patch, Post, Delete";
        private readonly IConfiguration Configuration;

        public PlayersController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // GET: api/<PlayersController>
        /// <summary>
        /// Return Implemetation Examples for the PlayerController
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public string Options()
        {
            // TODO: Impliment
            Response.StatusCode = 501; // Http 501 means not implimented
            return string.Empty;
        }

        // GET api/<PlayersController>
        /// <summary>
        /// Get user info for submitted id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet(Name = "Info")]
        public async Task<string> Get([FromHeader] Guid id)
        {



            // TODO: Impliment
            await Task.Delay(0);
            Response.StatusCode = 501; // Http 501 means not implimented
            return string.Empty;
        }

        // PATCH api/<PlayersController>
        /// <summary>
        /// Update record at id if it exists
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPatch]
        public async Task<bool> Patch([FromHeader] Guid id, [FromBody] string value)
        {
            // TODO: Impliment
            await Task.Delay(0);
            Response.StatusCode = 501; // Http 501 means not implimented
            return false;
        }

        // POST api/<PlayersController>
        /// <summary>
        /// Insert new player info and receive id
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Guid> Post([FromBody] string value)
        {
            // TODO: Impliment
            await Task.Delay(0);
            Response.StatusCode = 501; // Http 501 means not implimented
            return Guid.Empty;
        }

        // DELETE api/<PlayersController>/5
        /// <summary>
        /// Delete user with given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> Delete([FromHeader] Guid id)
        {
            // TODO: Impliment
            await Task.Delay(0);
            Response.StatusCode = 501; // Http 501 means not implimented
            return false;
        }

        #region Method Not Allowed

        // PUT api/<PlayersController>/5
        /// <summary>
        /// Submit user data to given id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut]
        public void Put()
        {
            Response.StatusCode = 405; // Http 405 means Method Not Allowed
            Response.Headers.Add("Allow", AllowedVerbs);
            Response.Body = null;
        }

        #endregion
    }
}
