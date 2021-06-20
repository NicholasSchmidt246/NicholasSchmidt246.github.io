using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Sudoku_WebService.Strategies;

namespace Sudoku_WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PuzzlesController : ControllerBase
    {
        private const string AllowedVerbs = "Get, Delete";
        private readonly IConfiguration Configuration;

        public PuzzlesController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // Options: api/<PuzzlesController>
        [HttpOptions]
        public void Options()
        {
            Response.Headers.Add("Allow", AllowedVerbs);
            Response.Body = null;
        }

        // GET api/<PuzzlesController>
        /// <summary>
        /// Returns an existing sudoku puzzle for a given "id"
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task Get([FromHeader] Guid userId, [FromHeader] Guid id, CancellationToken cancellationToken = new CancellationToken())
        {
            using var Play = new PlayStrategy(Configuration);
            Response.Body = await Play.GetPuzzle(Request.Headers["Accept"], userId, id, cancellationToken);
        }

        // GET api/<PuzzlesController>
        /// <summary>
        /// Create new puzzle, with difficulty and dimension
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet(Name = "New")]
        public async Task Get([FromHeader] Guid userId, [FromHeader] string difficulty, [FromHeader] int dimension, CancellationToken cancellationToken = new CancellationToken())
        {
            using var Play = new PlayStrategy(Configuration);
            Response.Body = await Play.GetPuzzle(Request.Headers["Accept"], userId, difficulty, dimension, cancellationToken);
        }

        // DELETE api/<PuzzlesController>
        /// <summary>
        /// Remove a puzzle with "id"
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        public async Task Delete([FromHeader] Guid userId, [FromHeader] Guid id, CancellationToken cancellationToken = new CancellationToken())
        {
            using var Play = new PlayStrategy(Configuration);
            await Play.DeletePuzzle(Request.Headers["Accept"], userId, id, cancellationToken);
        }

        #region Method Not Allowed

        // PATCH api/<PuzzlesController>
        /// <summary>
        /// Add or Replace a puzzle with "id"
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPatch]
        public void Patch()
        {
            Response.StatusCode = 405; // Http 405 means Method Not Allowed
            Response.Body = null;
            Response.Headers.Add("Allow", AllowedVerbs);
        }
        // POST api/<PuzzlesController>
        /// <summary>
        /// Submit a Puzzle
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public void Post()
        {
            Response.StatusCode = 405; // Http 405 means Method Not Allowed
            Response.Body = null;
            Response.Headers.Add("Allow", AllowedVerbs);
        }
        // PUT api/<PuzzlesController>
        /// <summary>
        /// Add or Replace a puzzle with "id"
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
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
