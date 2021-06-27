using InputInterpreter.Models;
using InputInterpreter.Helper;
using System.Collections.Generic;
using System;
using Takenet.Textc;
using System.Threading;

namespace InputInterpreter.Services
{
    public interface IInputInterpreterService
    {
        public ShapeInfo InterpretShapeInput(string input);
    }

    public class InputInterpreterService : IInputInterpreterService
    {
        public RequestContext _context;
        public ITextProcessor _textProcessor;

        public InputInterpreterService()
        {
            _context = new RequestContext();
            _textProcessor = InputStringInterpreter.CreateTextProcessor();
        }

        public ShapeInfo InterpretShapeInput(string input)
        {
            try
            {
                var task = _textProcessor.ProcessAsync(input, _context, CancellationToken.None);
                task.Wait();
            }
            catch
            {
                throw new Exception("Invalid Input: Input must be in the form 'Draw a(n) <shape> with a(n) <measurement> of <whole number> (and a(n) <measurement> of <whole number> ...)");
            }

            var shapeInfo = InputStringInterpreter.shapeInfo;
            ShapeValidator.ValidateShape(shapeInfo);
            ShapeCalculator.CalculateShape(shapeInfo);
            return shapeInfo;
        }


    }
}