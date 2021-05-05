using API.Errors;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class BuggyController : BaseAPIController
    {
        private readonly IProductRepository _repository;

        public BuggyController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("testauth")]
        [Authorize]
        public ActionResult<string> GetSecretText()
        {
            return "secret stuff";
        }

        [HttpGet("notfound")]
        public async Task<ActionResult> GetNotFoundRequest()
        {
            var thing = await _repository.GetProductByIdAsync(42);

            if (thing is null)
                return NotFound(new ApiResponse(404));

            return Ok();
        }

        [HttpGet("servererror")]
        public async Task<ActionResult> GetServerError()
        {
            var thing = await _repository.GetProductByIdAsync(42);

            var thingToReturn = thing.ToString();

            return Ok();
        }

        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {


            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badrequest/{id}")]
        public ActionResult GetNotFoundRequest(int id)
        {
            return Ok();
        }
    }
}
