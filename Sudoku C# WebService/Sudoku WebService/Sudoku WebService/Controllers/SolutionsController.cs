using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku_WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolutionsController : ControllerBase
    {
        private const string AllowedVerbs = "Get, Post";

        // GET: api/<SudokuSolverController>
        [HttpOptions]
        public string Options()
        {
            // TODO: Impliment
            Response.StatusCode = 501; // Http 501 means not implimented
            return string.Empty;
        }

        // GET api/<SudokuSolverController>
        [HttpGet]
        public async Task<string> Get([FromHeader] Guid userId, [FromHeader] Guid puzzleId, [FromHeader] bool full)
        {
            // TODO: Impliment
            await Task.Delay(0);
            Response.StatusCode = 501; // Http 501 means not implimented
            return string.Empty;
        }

        // POST api/<SudokuSolverController>
        [HttpPost]
        public async Task Post([FromHeader] Guid userId, [FromBody] string puzzle)
        {
            // TODO: Impliment
            await Task.Delay(0);
            Response.StatusCode = 501; // Http 501 means not implimented
        }

        #region Method Not Allowed

        // PATCH api/<SudokuSolverController>
        [HttpPatch]
        public void Patch()
        {
            Response.StatusCode = 405; // Http 405 means Method Not Allowed
            Response.Headers.Add("Allow", AllowedVerbs);
            Response.Body = null;
        }

        // PUT api/<SudokuSolverController>
        [HttpPut]
        public void Put()
        {
            Response.StatusCode = 405; // Http 405 means Method Not Allowed
            Response.Headers.Add("Allow", AllowedVerbs);
            Response.Body = null;
        }

        // DELETE api/<SudokuSolverController>
        [HttpDelete]
        public void Delete()
        {
            Response.StatusCode = 405; // Http 405 means Method Not Allowed
            Response.Headers.Add("Allow", AllowedVerbs);
            Response.Body = null;
        }

        #endregion
    }
}
