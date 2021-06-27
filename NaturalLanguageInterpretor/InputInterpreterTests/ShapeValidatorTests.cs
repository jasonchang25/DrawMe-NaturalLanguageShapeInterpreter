using Microsoft.VisualStudio.TestTools.UnitTesting;
using InputInterpreter.Helper;
using System.Threading;
using System;
using InputInterpreter.Models;
using System.Collections.Generic;

namespace InputInterpreterTests
{
    [TestClass]
    public class GeneralShapeValidatorTests
    {
        protected ShapeInfo CreateShapeInfo(string shape, Dictionary<string, int> information)
        {
            return new ShapeInfo()
            {
                Shape = shape,
                Information = information
            };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid Shape [{box}] specified")]
        public void InvalidShapeBox_Exception()
        {
            var information = new Dictionary<string, int>();
            information.Add("length", 100);
            var shape = CreateShapeInfo("box", information);
            ShapeValidator.ValidateShape(shape);
        }
    }

    [TestClass]
    public class SingleLengthShapesValidatorTests : GeneralShapeValidatorTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid number of measurement(s) provided for shape [square]. Only one measurement needs to be specified")]
        public void SingleLengthShapesZeroMeasurementsScenario_Exception()
        {
            var information = new Dictionary<string, int>();
            var shape = CreateShapeInfo("square", information);
            ShapeValidator.ValidateShape(shape);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid number of measurement(s) provided for shape [hexagon]. Only one measurement needs to be specified")]
        public void SingleLengthShapesTooManyMeasurementsScenario_Exception()
        {
            var information = new Dictionary<string, int>();
            information.Add("length", 100);
            information.Add("width", 100);
            var shape = CreateShapeInfo("hexagon", information);
            ShapeValidator.ValidateShape(shape);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid measurement(s) provided for shape [heptagon]. length must be provided")]
        public void SingleLengthShapesWrongMeasurementsScenario_Exception()
        {
            var information = new Dictionary<string, int>();
            information.Add("width", 100);
            var shape = CreateShapeInfo("heptagon", information);
            ShapeValidator.ValidateShape(shape);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid length amount provided. Value must be greater than 0")]
        public void SingleLengthShapesZeroLengthScenario_Exception()
        {
            var information = new Dictionary<string, int>();
            information.Add("length", 0);
            var shape = CreateShapeInfo("octagon", information);
            ShapeValidator.ValidateShape(shape);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid length amount provided. Value must be greater than 0")]
        public void SingleLengthShapesNegativeLengthScenario_Exception()
        {
            var information = new Dictionary<string, int>();
            information.Add("length", -1);
            var shape = CreateShapeInfo("octagon", information);
            ShapeValidator.ValidateShape(shape);
        }

        [TestMethod]
        public void SingleLengthShapesCorrectScenario_Pass()
        {
            var information = new Dictionary<string, int>();
            information.Add("length", 100);
            var shape = CreateShapeInfo("heptagon", information);
            ShapeValidator.ValidateShape(shape);
        }
    }

    [TestClass]
    public class WidthAndHeightShapesValidatorTests : GeneralShapeValidatorTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid number of measurement(s) provided for shape [isosceles triangle]. Only one measurement needs to be specified")]
        public void WidthAndHeightShapesZeroMeasurementsScenario_Exception()
        {
            var information = new Dictionary<string, int>();
            var shape = CreateShapeInfo("isosceles triangle", information);
            ShapeValidator.ValidateShape(shape);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid number of measurement(s) provided for shape [parallelogram]. Only one measurement needs to be specified")]
        public void WidthAndHeightShapesTooManyMeasurementsScenario_Exception()
        {
            var information = new Dictionary<string, int>();
            information.Add("length", 100);
            information.Add("width", 100);
            information.Add("height", 100);
            var shape = CreateShapeInfo("parallelogram", information);
            ShapeValidator.ValidateShape(shape);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid measurement(s) provided for shape [rectangle]. width must be provided")]
        public void WidthAndHeightShapesNoWidthScenario_Exception()
        {
            var information = new Dictionary<string, int>();
            information.Add("length", 100);
            information.Add("height", 100);
            var shape = CreateShapeInfo("rectangle", information);
            ShapeValidator.ValidateShape(shape);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid measurement(s) provided for shape [oval]. height must be provided")]
        public void WidthAndHeightShapesNoHeightScenario_Exception()
        {
            var information = new Dictionary<string, int>();
            information.Add("length", 100);
            information.Add("width", 100);
            var shape = CreateShapeInfo("oval", information);
            ShapeValidator.ValidateShape(shape);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid width amount provided. Value must be greater than 0")]
        public void WidthAndHeightShapesZeroWidthScenario_Exception()
        {
            var information = new Dictionary<string, int>();
            information.Add("height", 100);
            information.Add("width", 0);
            var shape = CreateShapeInfo("rectangle", information);
            ShapeValidator.ValidateShape(shape);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid width amount provided. Value must be greater than 0")]
        public void WidthAndHeightShapesNegativeWidthScenario_Exception()
        {
            var information = new Dictionary<string, int>();
            information.Add("height", 100);
            information.Add("width", -1);
            var shape = CreateShapeInfo("rectangle", information);
            ShapeValidator.ValidateShape(shape);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid height amount provided. Value must be height than 0")]
        public void WidthAndHeightShapesZeroHeightScenario_Exception()
        {
            var information = new Dictionary<string, int>();
            information.Add("height", 0);
            information.Add("width", 100);
            var shape = CreateShapeInfo("rectangle", information);
            ShapeValidator.ValidateShape(shape);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid height amount provided. Value must be height than 0")]
        public void WidthAndHeightShapesNegativeHeightScenario_Exception()
        {
            var information = new Dictionary<string, int>();
            information.Add("height", -1);
            information.Add("width", 100);
            var shape = CreateShapeInfo("rectangle", information);
            ShapeValidator.ValidateShape(shape);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid measurement(s) provided for shape [rectangle]. width and height amount can not be the same.")]
        public void SameHeightAndWidthRectangleScenario_Exception()
        {
            var information = new Dictionary<string, int>();
            information.Add("height", 100);
            information.Add("width", 100);
            var shape = CreateShapeInfo("rectangle", information);
            ShapeValidator.ValidateShape(shape);
        }

        [TestMethod]
        public void WidthAndHeightShapesCorrectMeasurementsScenario_Pass()
        {
            var information = new Dictionary<string, int>();
            information.Add("height", 60);
            information.Add("width", 100);
            var shape = CreateShapeInfo("isosceles triangle", information);
            ShapeValidator.ValidateShape(shape);
        }
    }

    [TestClass]
    public class CircleValidatorTests : GeneralShapeValidatorTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid number of measurement(s) provided for shape [circle]. Only radius needs to be specified")]
        public void CircleZeroMeasurementsScenario_Exception()
        {
            var information = new Dictionary<string, int>();
            var shape = CreateShapeInfo("circle", information);
            ShapeValidator.ValidateShape(shape);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid number of measurement(s) provided for shape [circle]. Only radius needs to be specified")]
        public void CircleTooManyMeasurementsScenario_Exception()
        {
            var information = new Dictionary<string, int>();
            information.Add("radius", 100);
            information.Add("width", 100);
            var shape = CreateShapeInfo("circle", information);
            ShapeValidator.ValidateShape(shape);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid measurement(s) provided for shape [circle]. radius must be provided")]
        public void CircleWrongMeasurementsScenario_Exception()
        {
            var information = new Dictionary<string, int>();
            information.Add("width", 100);
            var shape = CreateShapeInfo("circle", information);
            ShapeValidator.ValidateShape(shape);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid radius amount provided. Value must be greater than 0")]
        public void CircleZeroRadiusScenario_Exception()
        {
            var information = new Dictionary<string, int>();
            information.Add("radius", 0);
            var shape = CreateShapeInfo("circle", information);
            ShapeValidator.ValidateShape(shape);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid radius amount provided. Value must be greater than 0")]
        public void CircleNegativeLengthScenario_Exception()
        {
            var information = new Dictionary<string, int>();
            information.Add("radius", -1);
            var shape = CreateShapeInfo("circle", information);
            ShapeValidator.ValidateShape(shape);
        }

        [TestMethod]
        public void CircleCorrectScenario_Pass()
        {
            var information = new Dictionary<string, int>();
            information.Add("radius", 10);
            var shape = CreateShapeInfo("circle", information);
            ShapeValidator.ValidateShape(shape);
        }
    }

    [TestClass]
    public class ParallelogramValidatorTests : GeneralShapeValidatorTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid number of measurement(s) provided for shape [parallelogram]. Only 2 measurement lengtha and lengthb needs to be specified")]
        public void ScaleneTriangleZeroMeasurementsScenario_Exception()
        {
            var information = new Dictionary<string, int>();
            var shape = CreateShapeInfo("parallelogram", information);
            ShapeValidator.ValidateShape(shape);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid number of measurement(s) provided for shape [parallelogram]. Only 2 measurement lengtha and lengthb needs to be specified")]
        public void ParallelogramTooManyMeasurementsScenario_Exception()
        {
            var information = new Dictionary<string, int>();
            information.Add("radius", 100);
            information.Add("width", 100);
            information.Add("radius", 100);
            var shape = CreateShapeInfo("parallelogram", information);
            ShapeValidator.ValidateShape(shape);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid measurement(s) provided for shape [parallelogram]. lengtha must be provided")]
        public void ParallelogramNoLengthAMeasurementsScenario_Exception()
        {
            var information = new Dictionary<string, int>();
            information.Add("a", 90);
            information.Add("lengthb", 50);
            var shape = CreateShapeInfo("parallelogram", information);
            ShapeValidator.ValidateShape(shape);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid measurement(s) provided for shape [parallelogram]. lengthb must be provided")]
        public void ParallelogramNoLengthBMeasurementsScenario_Exception()
        {
            var information = new Dictionary<string, int>();
            information.Add("lengtha", 90);
            information.Add("b", 50);
            var shape = CreateShapeInfo("parallelogram", information);
            ShapeValidator.ValidateShape(shape);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid lengtha amount provided. Value must be greater than 0")]
        public void ParallelogramZeroLengthAScenario_Exception()
        {
            var information = new Dictionary<string, int>();
            information.Add("lengtha", 0);
            information.Add("lengthb", 50);
            var shape = CreateShapeInfo("parallelogram", information);
            ShapeValidator.ValidateShape(shape);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid lengtha amount provided. Value must be greater than 0")]
        public void ParallelogramNegativeLengthAScenario_Exception()
        {
            var information = new Dictionary<string, int>();
            information.Add("lengtha", -1);
            information.Add("lengthb", 50);
            var shape = CreateShapeInfo("parallelogram", information);
            ShapeValidator.ValidateShape(shape);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid lengthb amount provided. Value must be greater than 0")]
        public void ParallelogramZeroLengthBScenario_Exception()
        {
            var information = new Dictionary<string, int>();
            information.Add("lengtha", 90);
            information.Add("lengthb", 0);
            var shape = CreateShapeInfo("parallelogram", information);
            ShapeValidator.ValidateShape(shape);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid lengthb amount provided. Value must be greater than 0")]
        public void ParallelogramNegativeLengthBScenario_Exception()
        {
            var information = new Dictionary<string, int>();
            information.Add("lengtha", 90);
            information.Add("lengthb", -1);
            var shape = CreateShapeInfo("parallelogram", information);
            ShapeValidator.ValidateShape(shape);
        }

        [TestMethod]
        public void ParallelogramCorrectScenario_Pass()
        {
            var information = new Dictionary<string, int>();
            information.Add("lengtha", 90);
            information.Add("lengthb", 50);
            var shape = CreateShapeInfo("parallelogram", information);
            ShapeValidator.ValidateShape(shape);
        }
    }

    [TestClass]
    public class ScaleneTriangleValidatorTests : GeneralShapeValidatorTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid number of measurement(s) provided for shape [scalene triangle]. Only 3 measurement length [a,b,c] needs to be specified")]
        public void ScaleneTriangleZeroMeasurementsScenario_Exception()
        {
            var information = new Dictionary<string, int>();
            var shape = CreateShapeInfo("scalene triangle", information);
            ShapeValidator.ValidateShape(shape);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid number of measurement(s) provided for shape [scalene triangle]. Only 3 measurement length [a,b,c] needs to be specified")]
        public void ScaleneTriangleTooManyMeasurementsScenario_Exception()
        {
            var information = new Dictionary<string, int>();
            information.Add("radius", 100);
            information.Add("width", 100);
            information.Add("radius", 100);
            information.Add("x", 100);
            var shape = CreateShapeInfo("scalene triangle", information);
            ShapeValidator.ValidateShape(shape);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid measurement(s) provided for shape [scalene triangle]. lengtha must be provided")]
        public void ScaleneTriangleNoLengthAMeasurementsScenario_Exception()
        {
            var information = new Dictionary<string, int>();
            information.Add("a", 90);
            information.Add("lengthb", 50);
            information.Add("lengthc", 100);
            var shape = CreateShapeInfo("scalene triangle", information);
            ShapeValidator.ValidateShape(shape);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid measurement(s) provided for shape [scalene triangle]. lengthb must be provided")]
        public void ScaleneTriangleNoLengthBMeasurementsScenario_Exception()
        {
            var information = new Dictionary<string, int>();
            information.Add("lengtha", 90);
            information.Add("b", 50);
            information.Add("lengthc", 100);
            var shape = CreateShapeInfo("scalene triangle", information);
            ShapeValidator.ValidateShape(shape);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid measurement(s) provided for shape [scalene triangle]. lengthc must be provided")]
        public void ScaleneTriangleNoLengthCMeasurementsScenario_Exception()
        {
            var information = new Dictionary<string, int>();
            information.Add("lengtha", 90);
            information.Add("lengthb", 50);
            information.Add("c", 100);
            var shape = CreateShapeInfo("scalene triangle", information);
            ShapeValidator.ValidateShape(shape);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid lengtha amount provided. Value must be greater than 0")]
        public void ScaleneTriangleZeroLengthAScenario_Exception()
        {
            var information = new Dictionary<string, int>();
            information.Add("lengtha", 0);
            information.Add("lengthb", 50);
            information.Add("lengthc", 100);
            var shape = CreateShapeInfo("scalene triangle", information);
            ShapeValidator.ValidateShape(shape);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid lengtha amount provided. Value must be greater than 0")]
        public void ScaleneTriangleNegativeLengthAScenario_Exception()
        {
            var information = new Dictionary<string, int>();
            information.Add("lengtha", -1);
            information.Add("lengthb", 50);
            information.Add("lengthc", 100);
            var shape = CreateShapeInfo("scalene triangle", information);
            ShapeValidator.ValidateShape(shape);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid lengthb amount provided. Value must be greater than 0")]
        public void ScaleneTriangleZeroLengthBScenario_Exception()
        {
            var information = new Dictionary<string, int>();
            information.Add("lengtha", 90);
            information.Add("lengthb", 0);
            information.Add("lengthc", 100);
            var shape = CreateShapeInfo("scalene triangle", information);
            ShapeValidator.ValidateShape(shape);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid lengthb amount provided. Value must be greater than 0")]
        public void ScaleneTriangleNegativeLengthBScenario_Exception()
        {
            var information = new Dictionary<string, int>();
            information.Add("lengtha", 90);
            information.Add("lengthb", -1);
            information.Add("lengthc", 100);
            var shape = CreateShapeInfo("scalene triangle", information);
            ShapeValidator.ValidateShape(shape);
        }

        [ExpectedException(typeof(ArgumentException), "Invalid lengthc amount provided. Value must be greater than 0")]
        public void ScaleneTriangleZeroLengthCScenario_Exception()
        {
            var information = new Dictionary<string, int>();
            information.Add("lengtha", 90);
            information.Add("lengthb", 50);
            information.Add("lengthc", 0);
            var shape = CreateShapeInfo("scalene triangle", information);
            ShapeValidator.ValidateShape(shape);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid lengthc amount provided. Value must be greater than 0")]
        public void ScaleneTriangleNegativeLengthCScenario_Exception()
        {
            var information = new Dictionary<string, int>();
            information.Add("lengtha", 90);
            information.Add("lengthb", 50);
            information.Add("lengthc", -1);
            var shape = CreateShapeInfo("scalene triangle", information);
            ShapeValidator.ValidateShape(shape);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid lengths provided for shape [Scalene Triangle]. All side lengths must be different")]
        public void ScaleneTriangleSharedLengthsScenario1_Exception()
        {
            var information = new Dictionary<string, int>();
            information.Add("lengtha", 50);
            information.Add("lengthb", 50);
            information.Add("lengthc", 50);
            var shape = CreateShapeInfo("scalene triangle", information);
            ShapeValidator.ValidateShape(shape);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid lengths provided for shape [Scalene Triangle]. All side lengths must be different")]
        public void ScaleneTriangleSharedLengthsScenario2_Exception()
        {
            var information = new Dictionary<string, int>();
            information.Add("lengtha", 50);
            information.Add("lengthb", 50);
            information.Add("lengthc", 100);
            var shape = CreateShapeInfo("scalene triangle", information);
            ShapeValidator.ValidateShape(shape);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid lengths provided for shape [Scalene Triangle]. All side lengths must be different")]
        public void ScaleneTriangleSharedLengthsScenario3_Exception()
        {
            var information = new Dictionary<string, int>();
            information.Add("lengtha", 50);
            information.Add("lengthb", 100);
            information.Add("lengthc", 50);
            var shape = CreateShapeInfo("scalene triangle", information);
            ShapeValidator.ValidateShape(shape);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid lengths provided for shape [Scalene Triangle]. All side lengths must be different")]
        public void ScaleneTriangleSharedLengthsScenario4_Exception()
        {
            var information = new Dictionary<string, int>();
            information.Add("lengtha", 100);
            information.Add("lengthb", 50);
            information.Add("lengthc", 50);
            var shape = CreateShapeInfo("scalene triangle", information);
            ShapeValidator.ValidateShape(shape);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid lengths provided for shape [Scalene Triangle]. Impossible to make a closed triangle with lengths [1, 2, 100] provided")]
        public void ScaleneTriangleInvalidLengthsScenario4_Exception()
        {
            var information = new Dictionary<string, int>();
            information.Add("lengtha", 1);
            information.Add("lengthb", 2);
            information.Add("lengthc", 100);
            var shape = CreateShapeInfo("scalene triangle", information);
            ShapeValidator.ValidateShape(shape);
        }

        [TestMethod]
        public void ScaleneTriangleCorrectScenario_Pass()
        {
            var information = new Dictionary<string, int>();
            information.Add("lengtha", 90);
            information.Add("lengthb", 50);
            information.Add("lengthc", 100);
            var shape = CreateShapeInfo("scalene triangle", information);
            ShapeValidator.ValidateShape(shape);
        }
    }
}
