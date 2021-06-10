using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku_WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovesController : ControllerBase
    {
        private const string AllowedVerbs = "Get, Post, Delete";

        // GET: api/<MovesController>
        /// <summary>
        /// Return Implemetation Examples for the MovesController
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Get()
        {
            // TODO: Impliment
            Response.StatusCode = 501; // Http 501 means not Implimented 
            return string.Empty;
        }

        // GET api/<MovesController>
        /// <summary>
        /// This get will get the move count for given puzzle id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet(Name = "Count")]
        public async Task<int> Get([FromHeader] Guid playerId, [FromHeader] Guid puzzleId)
        {
            // TODO: Impliment
            await Task.Delay(0);
            Response.StatusCode = 501; // Http 501 means not Implimented 
            return int.MinValue;
        }

        // POST api/<MovesController>
        /// <summary>
        /// Insert a new move for puzzle id
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> Post([FromHeader] Guid userId, [FromHeader] Guid puzzleId, [FromBody] string value)
        {
            // TODO: Impliment
            await Task.Delay(0);
            Response.StatusCode = 501; // Http 501 means not Implimented 
            Response.Body = null;
            return false;
        }

        // DELETE api/<MovesController>/5
        /// <summary>
        /// Delete most recent move for given puzzle id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> Delete([FromHeader] Guid userId, [FromHeader] Guid puzzleId)
        {
            // TODO: Impliment
            await Task.Delay(0);
            Response.StatusCode = 501; // Http 501 means not Implimented 
            return false;
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
