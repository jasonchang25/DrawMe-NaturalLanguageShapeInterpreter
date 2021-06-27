using Microsoft.VisualStudio.TestTools.UnitTesting;
using InputInterpreter.Helper;
using Takenet.Textc;
using System.Threading;
using System.Threading.Tasks;
using System;
using Takenet.Textc.Processors;
using System.Diagnostics;
using InputInterpreter.Models;

namespace InputInterpreterTests
{
    [TestClass]
    public class InputStringInterpretorTests
    {
        protected RequestContext _context;
        protected ITextProcessor _textProcessor;

        [TestInitialize]
        public void Initialise()
        {
            _context = new RequestContext();
            _textProcessor = InputStringInterpreter.CreateTextProcessor();
        }

        // General Text Validation
        [TestMethod]
        [ExpectedException(typeof(AggregateException), "One or more errors occurred. (Match not found for user input)")]
        public void InvalidInputFullInvalidShould_Exception1()
        {
            var task = _textProcessor.ProcessAsync("apple", _context, CancellationToken.None);
            task.Wait();
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException), "One or more errors occurred. (Match not found for user input)")]
        public void InvalidInputFullInvalidShould_Exception2()
        {
            var task = _textProcessor.ProcessAsync("draw a circle with a radius", _context, CancellationToken.None);
            task.Wait();
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException), "One or more errors occurred. (Match not found for user input)")]
        public void InvalidInputInvalidAmountShould_Exception1()
        {
            var task = _textProcessor.ProcessAsync("draw a circle with a radius of twenty", _context, CancellationToken.None);
            task.Wait();
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException), "One or more errors occurred. (Match not found for user input)")]
        public void InvalidInputInvalidAmountShould_Exception2()
        {
            var task = _textProcessor.ProcessAsync("draw a circle with a radius of 0.1", _context, CancellationToken.None);
            task.Wait();
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException), "One or more errors occurred. (Match not found for user input)")]
        public void InvalidInputInvalidAmountShould_Exception3()
        {
            var task = _textProcessor.ProcessAsync("draw a circle with a radius of #", _context, CancellationToken.None);
            task.Wait();
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException), "One or more errors occurred. (Match not found for user input)")]
        public void InvalidInputInvalidMeasurementShould_Exception()
        {
            var task = _textProcessor.ProcessAsync("draw a circle with a big radius of 100", _context, CancellationToken.None);
            task.Wait();
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException), "One or more errors occurred. (Match not found for user input)")]
        public void InvalidInputFullInvalidShould_Exception3()
        {
            var task = _textProcessor.ProcessAsync("draw a circle with a radius of 100 extra words", _context, CancellationToken.None);
            task.Wait();
        }

        // Single Word Shape Single Input Scenarios
        [TestMethod]
        public void ValidInputSingleWordShapeSingleInput_Pass()
        {
            var task = _textProcessor.ProcessAsync("draw a circle with a radius of 100", _context, CancellationToken.None);
            task.Wait();
            var output = InputStringInterpreter.shapeInfo;
            Assert.IsTrue(output.Shape == "circle");
            Assert.IsTrue(output.Information.Count == 1);
            Assert.IsTrue(output.Information["radius"] == 100);
        }

        [TestMethod]
        public void ValidInputSingleWordShapeSingleInputWithSide_Pass()
        {
            var task = _textProcessor.ProcessAsync("draw a circle with a side radius of 100", _context, CancellationToken.None);
            task.Wait();
            var output = InputStringInterpreter.shapeInfo;
            Assert.IsTrue(output.Shape == "circle");
            Assert.IsTrue(output.Information.Count == 1);
            Assert.IsTrue(output.Information["radius"] == 100);
        }

        [TestMethod]
        // Invalid but will be covered in other validation steps
        public void ValidInputSingleWordShapeSingleInput2_Pass()
        {
            var task = _textProcessor.ProcessAsync("draw a invalidshape with a box of 1", _context, CancellationToken.None);
            task.Wait();
            var output = InputStringInterpreter.shapeInfo;
            Assert.IsTrue(output.Shape == "invalidshape");
            Assert.IsTrue(output.Information.Count == 1);
            Assert.IsTrue(output.Information["box"] == 1);
        }

        // Double Word Shape Single Input Scenarios
        [TestMethod]
        public void ValidInputDoubleWordShapeSingleInput_Pass()
        {
            var task = _textProcessor.ProcessAsync("draw a equilateral triangle with a length of 100", _context, CancellationToken.None);
            task.Wait();
            var output = InputStringInterpreter.shapeInfo;
            Assert.IsTrue(output.Shape == "equilateral triangle");
            Assert.IsTrue(output.Information.Count == 1);
            Assert.IsTrue(output.Information["length"] == 100);
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException), "One or more errors occurred. (Match not found for user input)")]
        public void InvalidInputFullInvalidShould_Exception4()
        {
            var task = _textProcessor.ProcessAsync("draw a equilateral triangle with a length of 100 extra words", _context, CancellationToken.None);
            task.Wait();
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException), "One or more errors occurred. (Match not found for user input)")]
        public void InvalidInputFullInvalidShould_Exception5()
        {
            var task = _textProcessor.ProcessAsync("draw a equilateral extra triangle with a length of 100", _context, CancellationToken.None);
            task.Wait();
        }

        // Single Word Shape Double Input Scenarios
        [TestMethod]
        public void ValidInputSingleWordShapeDoubleInput_Pass()
        {
            var task = _textProcessor.ProcessAsync("draw a rectangle with a side length of 100 and a height of 50", _context, CancellationToken.None);
            task.Wait();
            var output = InputStringInterpreter.shapeInfo;
            Assert.IsTrue(output.Shape == "rectangle");
            Assert.IsTrue(output.Information.Count == 2);
            Assert.IsTrue(output.Information["length"] == 100);
            Assert.IsTrue(output.Information["height"] == 50);
        }

        [TestMethod]
        public void ValidInputSingleWordShapeDoubleInput2_Pass()
        {
            var task = _textProcessor.ProcessAsync("draw a parallelogram with a height of 50 and a length of 100", _context, CancellationToken.None);
            task.Wait();
            var output = InputStringInterpreter.shapeInfo;
            Assert.IsTrue(output.Shape == "parallelogram");
            Assert.IsTrue(output.Information.Count == 2);
            Assert.IsTrue(output.Information["height"] == 50);
            Assert.IsTrue(output.Information["length"] == 100);
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException), "One or more errors occurred. (Match not found for user input)")]
        public void InvalidInputFullInvalidShould_Exception6()
        {
            var task = _textProcessor.ProcessAsync("draw a rectangle with a side length of 100 and height of 50", _context, CancellationToken.None);
            task.Wait();
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException), "One or more errors occurred. (Match not found for user input)")]
        public void InvalidInputFullInvalidShould_Exception7()
        {
            var task = _textProcessor.ProcessAsync("draw a rectangle with a side length of 100 and height of 50 extra", _context, CancellationToken.None);
            task.Wait();
        }

        // Double Word Shape Double Input Scenarios
        [TestMethod]
        public void ValidInputDoubleWordShapeDoubleInput_Pass()
        {
            var task = _textProcessor.ProcessAsync("draw a isosceles triangle with a width of 100 and a height of 200", _context, CancellationToken.None);
            task.Wait();
            var output = InputStringInterpreter.shapeInfo;
            Assert.IsTrue(output.Shape == "isosceles triangle");
            Assert.IsTrue(output.Information.Count == 2);
            Assert.IsTrue(output.Information["width"] == 100);
            Assert.IsTrue(output.Information["height"] == 200);
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException), "One or more errors occurred. (Match not found for user input)")]
        public void InvalidInputFullInvalidShould_Exception8()
        {
            var task = _textProcessor.ProcessAsync("draw a isosceles triangle with a width of 100 and a height of 200 extra", _context, CancellationToken.None);
            task.Wait();
        }

        // Double Word Shape Triple Input Scenarios
        [TestMethod]
        public void ValidInputDoubleWordShapeTripleInput_Pass()
        {
            var task = _textProcessor.ProcessAsync("draw a scalene triangle with a lengtha of 100 and a lengthb of 200 and a lengthc of 50", _context, CancellationToken.None);
            task.Wait();
            var output = InputStringInterpreter.shapeInfo;
            Assert.IsTrue(output.Shape == "scalene triangle");
            Assert.IsTrue(output.Information.Count == 3);
            Assert.IsTrue(output.Information["lengtha"] == 100);
            Assert.IsTrue(output.Information["lengthb"] == 200);
            Assert.IsTrue(output.Information["lengthc"] == 50);
        }

        [TestMethod]
        // Invalid but will be covered in other validation steps
        public void DictionaryScenarioSameMeasurmentNames_Pass()
        {
            var task = _textProcessor.ProcessAsync("draw a scalene triangle with a length of 100 and a length of 200 and a length of 50", _context, CancellationToken.None);
            task.Wait();
            var output = InputStringInterpreter.shapeInfo;
            Assert.IsTrue(output.Shape == "scalene triangle");
            Assert.IsTrue(output.Information.Count == 1);
            Assert.IsTrue(output.Information["length"] == 50);
        }
    }
}
