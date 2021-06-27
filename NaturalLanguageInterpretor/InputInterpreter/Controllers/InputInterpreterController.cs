using InputInterpreter.Helper;
using InputInterpreter.Models;
using InputInterpreter.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using Takenet.Textc;

namespace InputInterpreter.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class InputInterpreterController : ControllerBase
    {
        public IInputInterpreterService _inputInterpreterService;

        public InputInterpreterController(IInputInterpreterService inputInterpreterService)
        {
            _inputInterpreterService = inputInterpreterService;
        }

        [HttpPost]
        public IActionResult InterpretShapeInput(ShapeInput shapeInput)
        {
            try
            {
                var shapeInfo = _inputInterpreterService.InterpretShapeInput(shapeInput.Input?.ToLower());
                return Ok(shapeInfo);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
