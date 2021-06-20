using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

using Sudoku_WebService.Strategies;

namespace Sudoku_WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovesController : ControllerBase
    {
        private const string AllowedVerbs = "Get, Post, Delete";
        private readonly IConfiguration Configuration;

        public MovesController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // Options: api/<MovesController>
        [HttpOptions]
        public void Options()
        {
            Response.Headers.Add("Allow", AllowedVerbs);
            Response.Body = null;
        }

        // GET api/<MovesController>
        /// <summary>
        /// This get will get the move count for given puzzle id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task Get([FromHeader] Guid userId, [FromHeader] Guid puzzleId, CancellationToken cancellationToken = new CancellationToken())
        {
            using var Play = new PlayStrategy(Configuration);
            Response.Body = await Play.GetMoveCount(Request.Headers["Accept"], userId, puzzleId, cancellationToken);
        }

        // POST api/<MovesController>
        /// <summary>
        /// Insert a new move for puzzle id
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Post([FromHeader] Guid userId, [FromHeader] Guid puzzleId, [FromBody] string move, CancellationToken cancellationToken = new CancellationToken())
        {
            using var Play = new PlayStrategy(Configuration);
            Response.Body = await Play.MakeMove(Request.Headers["Content-Type"], Request.Headers["Accept"], userId, puzzleId, move, cancellationToken);
        }

        // DELETE api/<MovesController>/5
        /// <summary>
        /// Delete most recent move for given puzzle id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task Delete([FromHeader] Guid userId, [FromHeader] Guid puzzleId, CancellationToken cancellationToken = new CancellationToken())
        {
            using var Play = new PlayStrategy(Configuration);
            Response.Body = await Play.DeleteMove(Request.Headers["Accept"], userId, puzzleId, cancellationToken);
        }

        #region Method Not Allowed

        // PATCH api/<MovesController>
        /// <summary>
        /// Edit existing move for given puzzle id
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPatch]
        public void Patch()
        {
            Response.StatusCode = 405; // Http 405 means Method Not Allowed
            Response.Body = null;
            Response.Headers.Add("Allow", AllowedVerbs);
        }

        // PUT api/<MovesController>/5
        /// <summary>
        /// Replace a move or insert new if it does not exist
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut]
        public void Put()
        {
            Response.StatusCode = 405; // Http 405 means Method Not Allowed
            Response.Body = null;
            Response.Headers.Add("Allow", AllowedVerbs);
        }

        #endregion
    }
}
