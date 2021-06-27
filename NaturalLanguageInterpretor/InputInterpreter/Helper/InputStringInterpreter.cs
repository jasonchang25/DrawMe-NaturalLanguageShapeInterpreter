using InputInterpreter.Models;
using System;
using System.Threading.Tasks;
using Takenet.Textc;
using Takenet.Textc.Csdl;
using Takenet.Textc.PreProcessors;
using Takenet.Textc.Processors;
using Takenet.Textc.Splitters;
using System.Collections.Generic;

namespace InputInterpreter.Helper
{
    public interface IInputStringInterpreter
    {
        public static ITextProcessor CreateTextProcessor;
    }

    public class InputStringInterpreter : IInputStringInterpreter
    {
        public static ShapeInfo shapeInfo;

        // Real ugly probably better to split this into smaller methods like GetStringSyntax, GetProcessingFunc, BuildMeasurementCommandProcessor, but I got lazy
        // Also ran under the assumption length and side length mean the same thing.
        public static ITextProcessor CreateTextProcessor()
        {
            //Define a output processor that prints the command results to the console
            var outputProcessor = new DelegateOutputProcessor<ShapeInfo>((o, context) => {
                shapeInfo = o;
            });

            // 1. Single word shape - single measurement input
            // Covers: circle - radius, square - side length, octagon - side length, pentagon - side length, hexagon - side length, heptagon - side length,        
            // Syntax: 'draw a(n) <shape> with a(n) (side?) <measurement> of <amount>'
            var singleShapeSingleMeasurementSyntax = 
                CsdlParser.Parse("[operation+:Word(draw) :Word(a,an) shape:Word :Word(with) :Word(a,an) :Word?(side) measurement:Word :Word(of) amount:Integer]");

            Func<string, string, int, Task<ShapeInfo>> singleShapeSingleMeasurementFunc =
                (shape, measurement, amount) => Task.FromResult(new ShapeInfo()
                {
                    Shape = shape,
                    Information = new Dictionary<string, int>() { [measurement] = amount }
                });

            var singleShapeSingleMeasurementCommandProcessor = new DelegateCommandProcessor(
                singleShapeSingleMeasurementFunc,
                false,
                outputProcessor,
                singleShapeSingleMeasurementSyntax
                );

            // 2. Double word shape - single measurement input
            // Covers: equilateral triangle - side length
            // Syntax: 'draw a(n) <shape1> <shape2> with a(n) (side?) <measurement> of <amount>'
            var doubleShapeSingleMeasurementSyntax = 
                CsdlParser.Parse("[operation+:Word(draw) :Word(a,an) shape1:Word shape2:Word :Word(with) :Word(a,an) :Word?(side) measurement:Word :Word(of) amount:Integer]");

            Func<string, string, string, int, Task<ShapeInfo>> doubleShapeSingleMeasurementFunc =
                (shape1, shape2, measurement, amount) => Task.FromResult(new ShapeInfo()
                {
                    Shape = String.Concat(shape1, " ", shape2),
                    Information = new Dictionary<string, int>() { [measurement] = amount }
                });

            var doubleShapeSingleMeasurementCommandProcessor = new DelegateCommandProcessor(
               doubleShapeSingleMeasurementFunc,
               false,
               outputProcessor,
               doubleShapeSingleMeasurementSyntax
               );

            // 3. Single word shape - double measurement input
            // Covers: rectangle - height + width, oval - height + width, parallelogram - side lengtha + side lengthb
            // Syntax: 'draw a(n) <shape> with a(n) (side?) <measurement1> of <amount1> and a(n) <measurement2> of <amount2>
            var singleShapeDoubleMeasurementSyntax = 
                CsdlParser.Parse("[operation+:Word(draw) :Word(a,an) shape:Word :Word(with) :Word(a,an) :Word?(side) measurement1:Word :Word(of) amount1:Integer " +
                                 ":Word(and) :Word(a,an) :Word?(side) measurement2:Word :Word(of) amount2:Integer]");

            Func<string, string, int, string, int, Task<ShapeInfo>> singleShapeDoubleMeasurementFunc =
                (shape, measurement1, amount1, measurement2, amount2) => Task.FromResult(new ShapeInfo()
                {
                    Shape = shape,
                    Information = new Dictionary<string, int>() { [measurement1] = amount1, [measurement2] = amount2 }
                });

            var singleShapeDoubleMeasurementCommandProcessor = new DelegateCommandProcessor(
               singleShapeDoubleMeasurementFunc,
               false,
               outputProcessor,
               singleShapeDoubleMeasurementSyntax
               );

            // 4. Double word shape - double measurement input
            // Covers: isoceles triangle - height + length 
            // Syntax: 'draw a(n) <shape1> <shape2> with a(n) (side?) <measurement1> of <amount1> and a(n) <measurement2> of <amount2>
            var doubleShapeDoubleMeasurementSyntax =
                CsdlParser.Parse("[operation+:Word(draw) :Word(a,an) shape1:Word shape2:Word :Word(with) :Word(a,an) :Word?(side) measurement1:Word :Word(of) amount1:Integer " +
                                 ":Word(and) :Word(a,an) :Word?(side) measurement2:Word :Word(of) amount2:Integer]");

            Func<string, string, string, int, string, int, Task<ShapeInfo>> doubleShapeDoubleMeasurementFunc =
                (shape1, shape2, measurement1, amount1, measurement2, amount2) => Task.FromResult(new ShapeInfo()
                {
                    Shape = String.Concat(shape1, " ", shape2),
                    Information = new Dictionary<string, int>() { [measurement1] = amount1, [measurement2] = amount2 }
                });

            var doubleShapeDoubleMeasurementCommandProcessor = new DelegateCommandProcessor(
               doubleShapeDoubleMeasurementFunc,
               false,
               outputProcessor,
               doubleShapeDoubleMeasurementSyntax
               );

            // 5.Double word shape - triple measurement input
            // Covers: scalene triangle - side lengthA, side lengthB, side lengthC
            // Syntax: 'draw a(n) <shape1> <shape2> with a(n) (side?) <measurement1> of <amount1> and a(n) (side?) <measurement2> of <amount2> and a(n) (side?) <measurement3> of <amount3>
            var doubleShapeTripleMeasurementSyntax =
                CsdlParser.Parse("[operation+:Word(draw) :Word(a,an) shape1:Word shape2:Word :Word(with) :Word(a,an) :Word?(side) measurement1:Word :Word(of) amount1:Integer " +
                                 ":Word(and) :Word(a,an) :Word?(side) measurement2:Word :Word(of) amount2:Integer :Word(and) :Word(a,an) :Word?(side) measurement3:Word " +
                                 ":Word(of) amount3:Integer]");

            Func<string, string, string, int, string, int, string, int, Task<ShapeInfo>> doubleShapeTripleMeasurementFunc =
                (shape1, shape2, measurement1, amount1, measurement2, amount2, measurement3, amount3) => Task.FromResult(new ShapeInfo()
                {
                    Shape = String.Concat(shape1, " ", shape2),
                    Information = new Dictionary<string, int>() { [measurement1] = amount1, [measurement2] = amount2, [measurement3] = amount3 }
                });

            var doubleShapeTripleMeasurementCommandProcessor = new DelegateCommandProcessor(
               doubleShapeTripleMeasurementFunc,
               false,
               outputProcessor,
               doubleShapeTripleMeasurementSyntax
               );

            // Finally, create the text processor and register all command processors
            var textProcessor = new TextProcessor(new PunctuationTextSplitter());
            textProcessor.CommandProcessors.Add(singleShapeSingleMeasurementCommandProcessor);
            textProcessor.CommandProcessors.Add(doubleShapeSingleMeasurementCommandProcessor);
            textProcessor.CommandProcessors.Add(singleShapeDoubleMeasurementCommandProcessor);
            textProcessor.CommandProcessors.Add(doubleShapeDoubleMeasurementCommandProcessor);
            textProcessor.CommandProcessors.Add(doubleShapeTripleMeasurementCommandProcessor);
            textProcessor.TextPreprocessors.Add(new TrimTextPreprocessor());
            return textProcessor;
        }
    }
}