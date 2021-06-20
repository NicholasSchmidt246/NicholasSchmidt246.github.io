using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

using Microsoft.Extensions.Configuration;

using Sudoku_WebService.Strategies;

namespace Sudoku_WebService.Controllers
{
    /// <summary>
    /// This does not use authentication, as this restricts no one, but it will 
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

        // Options: api/<PlayersController>
        [HttpOptions]
        public void Options()
        {
            Response.Headers.Add("Allow", AllowedVerbs);
            Response.Body = null;
        }

        // GET api/<PlayersController>
        /// <summary>
        /// Get user info for submitted id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet(Name = "Info")]
        public async Task Get([FromHeader] Guid id, CancellationToken cancellationToken = new CancellationToken())
        {
            using var Auth = new AuthorizationStrategy(Configuration);
            Response.Body = await Auth.GetUserData(Request.Headers["Accept"], id, cancellationToken);
        }

        // PATCH api/<PlayersController>
        /// <summary>
        /// Update record at id if it exists
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPatch]
        public async Task Patch([FromHeader] Guid id, [FromBody] string userData, CancellationToken cancellationToken = new CancellationToken())
        {
            using var Auth = new AuthorizationStrategy(Configuration);
            Response.Body = await Auth.UpdateUserData(Request.Headers["Content-Type"], Request.Headers["Accept"], userData, cancellationToken);
        }

        // POST api/<PlayersController>
        /// <summary>
        /// Insert new player info and receive id
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Post([FromBody] string userData, CancellationToken cancellationToken = new CancellationToken())
        {
            using var Auth = new AuthorizationStrategy(Configuration);
            Response.Body = await Auth.AddUserData(Request.Headers["Content-Type"], Request.Headers["Accept"], userData, cancellationToken);
        }

        // DELETE api/<PlayersController>/5
        /// <summary>
        /// Delete user with given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task Delete([FromHeader] Guid id, CancellationToken cancellationToken = new CancellationToken())
        {
            using var Auth = new AuthorizationStrategy(Configuration);
            Response.Body = await Auth.DeleteUserData(Request.Headers["Accept"], id, cancellationToken);
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
