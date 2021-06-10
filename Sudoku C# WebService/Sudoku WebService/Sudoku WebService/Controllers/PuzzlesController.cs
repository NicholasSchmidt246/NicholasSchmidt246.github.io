//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;

//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;

//using Sudoku_WebService.Strategies;

//namespace Sudoku_WebService.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class PuzzlesController : ControllerBase
//    {
//        private const string AllowedVerbs = "Get, Delete";

//        // GET: api/<PuzzlesController>
//        /// <summary>
//        /// Return Implemetation Examples for the PuzzlesController
//        /// </summary>
//        /// <returns></returns>
//        [HttpGet]
//        public string Get()
//        {
//            // TODO: Impliment
//            Response.StatusCode = 501; // Http 501 means not Implimented 
//            return string.Empty;
//        }

//        // GET api/<PuzzlesController>
//        /// <summary>
//        /// Returns an existing sudoku puzzle for a given "id"
//        /// </summary>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        [HttpGet(Name = "Existing")]
//        public async Task Get([FromHeader] Guid userId, [FromHeader] Guid id)
//        {
//            using (var Auth = new AuthorizationStrategy(userId))
//            {
//                if(!await Auth.IsAuthorized(userId))
//                {
//                    Response.StatusCode = 403; // Http 403 means Forbidden, which is the actual message you want to send to an unauthorized individual. Http 401 unauthorized (despite it's name) is used for unauthenticated
//                }
//            }

//            using var Play = new PlayStrategy(userId, id);
//            string ContentType = Request.Headers["Content-Type"];
//            Response.Body = await Play.GetPuzzle(ContentType);
//        }

//        // GET api/<PuzzlesController>
//        /// <summary>
//        /// Create new puzzle, with difficulty and dimension
//        /// </summary>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        [HttpGet(Name = "New")]
//        public async Task Get([FromHeader] Guid userId, [FromHeader] string difficulty, [FromHeader] int dimension)
//        {
//            using (var Auth = new AuthorizationStrategy(userId))
//            {
//                if (!await Auth.IsAuthorized(userId))
//                {
//                    Response.StatusCode = 403; // Http 403 means Forbidden, which is the actual message you want to send to an unauthorized individual. Http 401 unauthorized (despite it's name) is used for unauthenticated
//                }
//            }

//            using var Play = new PlayStrategy(userId);
//            string ContentType = Request.Headers["Content-Type"];
//            Response.Body = await Play.GetPuzzle(ContentType, difficulty, dimension);
//        }

//        // DELETE api/<PuzzlesController>
//        /// <summary>
//        /// Remove a puzzle with "id"
//        /// </summary>
//        /// <param name="id"></param>
//        [HttpDelete]
//        public async Task Delete([FromHeader] Guid userId, [FromHeader] Guid id)
//        {
//            using (var Auth = new AuthorizationStrategy(userId))
//            {
//                if (!await Auth.IsAuthorized(userId))
//                {
//                    Response.StatusCode = 403; // Http 403 means Forbidden, which is the actual message you want to send to an unauthorized individual. Http 401 unauthorized (despite it's name) is used for unauthenticated
//                }
//            }

//            using var Play = new PlayStrategy(userId, id);
//            string ContentType = Request.Headers["Content-Type"];
//            await Play.DeletePuzzle(ContentType);
//        }

//        #region Method Not Allowed

//        // PATCH api/<PuzzlesController>
//        /// <summary>
//        /// Add or Replace a puzzle with "id"
//        /// </summary>
//        /// <param name="id"></param>
//        /// <param name="value"></param>
//        [HttpPatch]
//        public void Patch()
//        {
//            Response.StatusCode = 405; // Http 405 means Method Not Allowed
//            Response.Body = null;
//            Response.Headers.Add("Allow", AllowedVerbs);
//        }
//        // POST api/<PuzzlesController>
//        /// <summary>
//        /// Submit a Puzzle
//        /// </summary>
//        /// <param name="value"></param>
//        [HttpPost]
//        public void Post()
//        {
//            Response.StatusCode = 405; // Http 405 means Method Not Allowed
//            Response.Body = null;
//            Response.Headers.Add("Allow", AllowedVerbs);
//        }
//        // PUT api/<PuzzlesController>
//        /// <summary>
//        /// Add or Replace a puzzle with "id"
//        /// </summary>
//        /// <param name="id"></param>
//        /// <param name="value"></param>
//        [HttpPut]
//        public void Put()
//        {
//            Response.StatusCode = 405; // Http 405 means Method Not Allowed
//            Response.Body = null;
//            Response.Headers.Add("Allow", AllowedVerbs);
//        }

//        #endregion
//    }
//}
