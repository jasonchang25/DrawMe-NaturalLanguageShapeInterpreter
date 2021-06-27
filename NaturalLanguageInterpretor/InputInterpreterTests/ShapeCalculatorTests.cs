using Microsoft.VisualStudio.TestTools.UnitTesting;
using InputInterpreter.Helper;
using System.Threading;
using System;
using InputInterpreter.Models;
using System.Collections.Generic;

namespace InputInterpreterTests
{
    public class ShapeCalculatorTests
    {
        protected ShapeInfo CreateShapeInfo(string shape, Dictionary<string, int> information, List<Coordinate> coordinates)
        {
            return new ShapeInfo()
            {
                Shape = shape,
                Information = information,
                ShapeVertices = coordinates
            };
        }

        protected void ValidateCoordinate(Coordinate coordinate, double expectedX, double expectedY)
        {
            Assert.IsTrue(coordinate.X == expectedX);
            Assert.IsTrue(coordinate.Y == expectedY);
        }
    }

    [TestClass]
    public class CircleCalculatorTests : ShapeCalculatorTests
    {
        [TestMethod]
        public void CalculateCircle_1()
        {
            var information = new Dictionary<string, int>() { { "radius", 20 } };
            var shape = CreateShapeInfo("circle", information, new List<Coordinate>());
            ShapeCalculator.CalculateShape(shape);
            Assert.IsTrue(shape.ShapeVertices.Count == 0);
        }

        [TestMethod]
        public void CalculateCircle_2()
        {
            var information = new Dictionary<string, int>() { { "radius", 150 } };
            var shape = CreateShapeInfo("circle", information, new List<Coordinate>());
            ShapeCalculator.CalculateShape(shape);
            Assert.IsTrue(shape.ShapeVertices.Count == 0);
        }
    }

    [TestClass]
    public class OvalCalculatorTests : ShapeCalculatorTests
    {
        [TestMethod]
        public void CalculateOval_1()
        {
            var information = new Dictionary<string, int>() { { "width", 200 }, { "height", 100 } };
            var shape = CreateShapeInfo("oval", information, new List<Coordinate>());
            ShapeCalculator.CalculateShape(shape);
            Assert.IsTrue(shape.ShapeVertices.Count == 0);
        }

        [TestMethod]
        public void CalculateOval_2()
        {
            var information = new Dictionary<string, int>() { { "width", 50 }, { "height", 90 } };
            var shape = CreateShapeInfo("oval", information, new List<Coordinate>());
            ShapeCalculator.CalculateShape(shape);
            Assert.IsTrue(shape.ShapeVertices.Count == 0);
        }
    }

    [TestClass]
    public class IsoscelesTriangleCalculatorTests : ShapeCalculatorTests
    {
        [TestMethod]
        public void CalculateIsocelesTriangle_1()
        {
            var information = new Dictionary<string, int>() { { "width", 200 }, { "height", 100 } };
            var shape = CreateShapeInfo("isosceles triangle", information, new List<Coordinate>());
            ShapeCalculator.CalculateShape(shape);
            Assert.IsTrue(shape.ShapeVertices.Count == 4);
            ValidateCoordinate(shape.ShapeVertices[0], -100, -50);
            ValidateCoordinate(shape.ShapeVertices[1], 100, -50);
            ValidateCoordinate(shape.ShapeVertices[2], 0, 50);
            ValidateCoordinate(shape.ShapeVertices[3], -100, -50);
        }

        [TestMethod]
        public void CalculateIsocelesTriangle_2()
        {
            var information = new Dictionary<string, int>() { { "width", 50 }, { "height", 170 } };
            var shape = CreateShapeInfo("isosceles triangle", information, new List<Coordinate>());
            ShapeCalculator.CalculateShape(shape);
            Assert.IsTrue(shape.ShapeVertices.Count == 4);
            ValidateCoordinate(shape.ShapeVertices[0], -25, -85);
            ValidateCoordinate(shape.ShapeVertices[1], 25, -85);
            ValidateCoordinate(shape.ShapeVertices[2], 0, 85);
            ValidateCoordinate(shape.ShapeVertices[3], -25, -85);
        }
    }

    [TestClass]
    public class SquareCalculatorTests : ShapeCalculatorTests
    {
        [TestMethod]
        public void CalculateSquare_1()
        {
            var information = new Dictionary<string, int>() { { "length", 200 } };
            var shape = CreateShapeInfo("square", information, new List<Coordinate>());
            ShapeCalculator.CalculateShape(shape);
            Assert.IsTrue(shape.ShapeVertices.Count == 5);
            ValidateCoordinate(shape.ShapeVertices[0], -100, -100);
            ValidateCoordinate(shape.ShapeVertices[1], 100, -100);
            ValidateCoordinate(shape.ShapeVertices[2], 100, 100);
            ValidateCoordinate(shape.ShapeVertices[3], -100, 100);
            ValidateCoordinate(shape.ShapeVertices[4], -100, -100);
        }

        [TestMethod]
        public void CalculateSquare_2()
        {
            var information = new Dictionary<string, int>() { { "length", 100 } };
            var shape = CreateShapeInfo("square", information, new List<Coordinate>());
            ShapeCalculator.CalculateShape(shape);
            Assert.IsTrue(shape.ShapeVertices.Count == 5);
            ValidateCoordinate(shape.ShapeVertices[0], -50, -50);
            ValidateCoordinate(shape.ShapeVertices[1], 50, -50);
            ValidateCoordinate(shape.ShapeVertices[2], 50, 50);
            ValidateCoordinate(shape.ShapeVertices[3], -50, 50);
            ValidateCoordinate(shape.ShapeVertices[4], -50, -50);
        }
    }

    [TestClass]
    public class ScaleneTriangleCalculatorTests : ShapeCalculatorTests
    {
        [TestMethod]
        public void CalculateScaleneTriangle_1()
        {
            var information = new Dictionary<string, int>() { { "lengtha", 500 }, { "lengthb", 400 }, { "lengthc", 300 } };
            var shape = CreateShapeInfo("scalene triangle", information, new List<Coordinate>());
            ShapeCalculator.CalculateShape(shape);
            Assert.IsTrue(shape.ShapeVertices.Count == 4);
            ValidateCoordinate(shape.ShapeVertices[0], -250, -120);
            ValidateCoordinate(shape.ShapeVertices[1], 250, -120);
            ValidateCoordinate(shape.ShapeVertices[2], -70, 120);
            ValidateCoordinate(shape.ShapeVertices[3], -250, -120);
        }

        [TestMethod]
        public void CalculateScaleneTriangle_2()
        {
            var information = new Dictionary<string, int>() { { "lengtha", 90 }, { "lengthb", 50 }, { "lengthc", 100 } };
            var shape = CreateShapeInfo("scalene triangle", information, new List<Coordinate>());
            ShapeCalculator.CalculateShape(shape);
            Assert.IsTrue(shape.ShapeVertices.Count == 4);
            ValidateCoordinate(shape.ShapeVertices[0], -45, -24.94);
            ValidateCoordinate(shape.ShapeVertices[1], 45, -24.94);
            ValidateCoordinate(shape.ShapeVertices[2], 41.67, 24.94);
            ValidateCoordinate(shape.ShapeVertices[3], -45, -24.94);
        }
    }

    [TestClass]
    public class ParallelogramCalculatorTests : ShapeCalculatorTests
    {
        [TestMethod]
        public void CalculateParallelogram_1()
        {
            var information = new Dictionary<string, int>() { { "lengtha", 500 }, { "lengthb", 400 } };
            var shape = CreateShapeInfo("parallelogram", information, new List<Coordinate>());
            ShapeCalculator.CalculateShape(shape);
            Assert.IsTrue(shape.ShapeVertices.Count == 5);
            ValidateCoordinate(shape.ShapeVertices[0], -250, -141.42);
            ValidateCoordinate(shape.ShapeVertices[1], 250, -141.42);
            ValidateCoordinate(shape.ShapeVertices[2], 532.84, 141.42);
            ValidateCoordinate(shape.ShapeVertices[3], 32.84, 141.42);
            ValidateCoordinate(shape.ShapeVertices[4], -250, -141.42);
        }

        [TestMethod]
        public void CalculateParallelogram_2()
        {
            var information = new Dictionary<string, int>() { { "lengtha", 100 }, { "lengthb", 200 } };
            var shape = CreateShapeInfo("parallelogram", information, new List<Coordinate>());
            ShapeCalculator.CalculateShape(shape);
            Assert.IsTrue(shape.ShapeVertices.Count == 5);
            ValidateCoordinate(shape.ShapeVertices[0], -50, -70.71);
            ValidateCoordinate(shape.ShapeVertices[1], 50, -70.71);
            ValidateCoordinate(shape.ShapeVertices[2], 191.42, 70.71);
            ValidateCoordinate(shape.ShapeVertices[3], 91.42, 70.71);
            ValidateCoordinate(shape.ShapeVertices[4], -50, -70.71);
        }
    }

    [TestClass]
    public class EquilateralTriangleCalculatorTests : ShapeCalculatorTests
    {
        [TestMethod]
        public void CalculateEquilateralTriangle_1()
        {
            var information = new Dictionary<string, int>() { { "length", 100 } };
            var shape = CreateShapeInfo("equilateral triangle", information, new List<Coordinate>());
            ShapeCalculator.CalculateShape(shape);
            Assert.IsTrue(shape.ShapeVertices.Count == 4);
            ValidateCoordinate(shape.ShapeVertices[0], -50, -37.5);
            ValidateCoordinate(shape.ShapeVertices[1], 50, -37.5);
            ValidateCoordinate(shape.ShapeVertices[2], 0, 37.5);
            ValidateCoordinate(shape.ShapeVertices[3], -50, -37.5);
        }

        [TestMethod]
        public void CalculateEquilateralTriangle_2()
        {
            var information = new Dictionary<string, int>() { { "length", 150 } };
            var shape = CreateShapeInfo("equilateral triangle", information, new List<Coordinate>());
            ShapeCalculator.CalculateShape(shape);
            Assert.IsTrue(shape.ShapeVertices.Count == 4);
            ValidateCoordinate(shape.ShapeVertices[0], -75, -56.25);
            ValidateCoordinate(shape.ShapeVertices[1], 75, -56.25);
            ValidateCoordinate(shape.ShapeVertices[2], 0, 56.25);
            ValidateCoordinate(shape.ShapeVertices[3], -75, -56.25);
        }
    }

    [TestClass]
    public class PentagonCalculatorTests : ShapeCalculatorTests
    {
        [TestMethod]
        public void CalculatePentagon_1()
        {
            var information = new Dictionary<string, int>() { { "length", 100 } };
            var shape = CreateShapeInfo("pentagon", information, new List<Coordinate>());
            ShapeCalculator.CalculateShape(shape);
            Assert.IsTrue(shape.ShapeVertices.Count == 6);
            ValidateCoordinate(shape.ShapeVertices[0], 85.07, 0);
            ValidateCoordinate(shape.ShapeVertices[1], 26.29, 80.90);
            ValidateCoordinate(shape.ShapeVertices[2], -68.82, 50.00);
            ValidateCoordinate(shape.ShapeVertices[3], -68.82, -50.00);
            ValidateCoordinate(shape.ShapeVertices[4], 26.29, -80.90);
            ValidateCoordinate(shape.ShapeVertices[5], 85.07, 0);
        }

        [TestMethod]
        public void CalculatePentagon_2()
        {
            var information = new Dictionary<string, int>() { { "length", 150 } };
            var shape = CreateShapeInfo("pentagon", information, new List<Coordinate>());
            ShapeCalculator.CalculateShape(shape);
            Assert.IsTrue(shape.ShapeVertices.Count == 6);
            ValidateCoordinate(shape.ShapeVertices[0], 127.6, 0);
            ValidateCoordinate(shape.ShapeVertices[1], 39.43, 121.35);
            ValidateCoordinate(shape.ShapeVertices[2], -103.23, 75.00);
            ValidateCoordinate(shape.ShapeVertices[3], -103.23, -75);
            ValidateCoordinate(shape.ShapeVertices[4], 39.43, -121.35);
            ValidateCoordinate(shape.ShapeVertices[5], 127.6, 0);
        }
    }

    [TestClass]
    public class RectangleCalculatorTests : ShapeCalculatorTests
    {
        [TestMethod]
        public void CalculatRectangle_1()
        {
            var information = new Dictionary<string, int>() { { "width", 200 }, { "height", 100 } };
            var shape = CreateShapeInfo("rectangle", information, new List<Coordinate>());
            ShapeCalculator.CalculateShape(shape);
            Assert.IsTrue(shape.ShapeVertices.Count == 5);
            ValidateCoordinate(shape.ShapeVertices[0], -100, -50);
            ValidateCoordinate(shape.ShapeVertices[1], 100, -50);
            ValidateCoordinate(shape.ShapeVertices[2], 100, 50);
            ValidateCoordinate(shape.ShapeVertices[3], -100, 50);
            ValidateCoordinate(shape.ShapeVertices[4], -100, -50);
        }

        [TestMethod]
        public void CalculatRectangle_2()
        {
            var information = new Dictionary<string, int>() { { "width", 100 }, { "height", 200 } };
            var shape = CreateShapeInfo("rectangle", information, new List<Coordinate>());
            ShapeCalculator.CalculateShape(shape);
            Assert.IsTrue(shape.ShapeVertices.Count == 5);
            ValidateCoordinate(shape.ShapeVertices[0], -50, -100);
            ValidateCoordinate(shape.ShapeVertices[1], 50, -100);
            ValidateCoordinate(shape.ShapeVertices[2], 50, 100);
            ValidateCoordinate(shape.ShapeVertices[3], -50, 100);
            ValidateCoordinate(shape.ShapeVertices[4], -50, -100);
        }
    }

    [TestClass]
    public class HexagonCalculatorTests : ShapeCalculatorTests
    {
        [TestMethod]
        public void CalculatHexagon_1()
        {
            var information = new Dictionary<string, int>() { { "length", 100 } };
            var shape = CreateShapeInfo("hexagon", information, new List<Coordinate>());
            ShapeCalculator.CalculateShape(shape);
            Assert.IsTrue(shape.ShapeVertices.Count == 7);
            ValidateCoordinate(shape.ShapeVertices[0], 100, 0);
            ValidateCoordinate(shape.ShapeVertices[1], 50, 86.6);
            ValidateCoordinate(shape.ShapeVertices[2], -50, 86.6);
            ValidateCoordinate(shape.ShapeVertices[3], -100, 0);
            ValidateCoordinate(shape.ShapeVertices[4], -50, -86.6);
            ValidateCoordinate(shape.ShapeVertices[5], 50, -86.6);
            ValidateCoordinate(shape.ShapeVertices[6], 100, 0);
        }

        [TestMethod]
        public void CalculatHexagon_2()
        {
            var information = new Dictionary<string, int>() { { "length", 150 } };
            var shape = CreateShapeInfo("hexagon", information, new List<Coordinate>());
            ShapeCalculator.CalculateShape(shape);
            Assert.IsTrue(shape.ShapeVertices.Count == 7);
            ValidateCoordinate(shape.ShapeVertices[0], 150, 0);
            ValidateCoordinate(shape.ShapeVertices[1], 75, 129.9);
            ValidateCoordinate(shape.ShapeVertices[2], -75, 129.9);
            ValidateCoordinate(shape.ShapeVertices[3], -150, 0);
            ValidateCoordinate(shape.ShapeVertices[4], -75, -129.9);
            ValidateCoordinate(shape.ShapeVertices[5], 75, -129.9);
            ValidateCoordinate(shape.ShapeVertices[6], 150, 0);
        }
    }

    [TestClass]
    public class HeptagonCalculatorTests : ShapeCalculatorTests
    {
        [TestMethod]
        public void CalculatHeptagon_1()
        {
            var information = new Dictionary<string, int>() { { "length", 100 } };
            var shape = CreateShapeInfo("heptagon", information, new List<Coordinate>());
            ShapeCalculator.CalculateShape(shape);
            Assert.IsTrue(shape.ShapeVertices.Count == 8);
            ValidateCoordinate(shape.ShapeVertices[0], 115.24, 0);
            ValidateCoordinate(shape.ShapeVertices[1], 71.85, 90.1);
            ValidateCoordinate(shape.ShapeVertices[2], -25.64, 112.35);
            ValidateCoordinate(shape.ShapeVertices[3], -103.83, 50);
            ValidateCoordinate(shape.ShapeVertices[4], -103.83, -50);
            ValidateCoordinate(shape.ShapeVertices[5], -25.64, -112.35);
            ValidateCoordinate(shape.ShapeVertices[6], 71.85, -90.1);
            ValidateCoordinate(shape.ShapeVertices[7], 115.24, 0);
        }

        [TestMethod]
        public void CalculatHeptagon_2()
        {
            var information = new Dictionary<string, int>() { { "length", 150 } };
            var shape = CreateShapeInfo("heptagon", information, new List<Coordinate>());
            ShapeCalculator.CalculateShape(shape);
            Assert.IsTrue(shape.ShapeVertices.Count == 8);
            ValidateCoordinate(shape.ShapeVertices[0], 172.86, 0);
            ValidateCoordinate(shape.ShapeVertices[1], 107.77, 135.15);
            ValidateCoordinate(shape.ShapeVertices[2], -38.46, 168.52);
            ValidateCoordinate(shape.ShapeVertices[3], -155.74, 75);
            ValidateCoordinate(shape.ShapeVertices[4], -155.74, -75);
            ValidateCoordinate(shape.ShapeVertices[5], -38.46, -168.52);
            ValidateCoordinate(shape.ShapeVertices[6], 107.77, -135.15);
            ValidateCoordinate(shape.ShapeVertices[7], 172.86, 0);
        }
    }

    [TestClass]
    public class OctagonCalculatorTests : ShapeCalculatorTests
    {
        [TestMethod]
        public void CalculatOctagon_1()
        {
            var information = new Dictionary<string, int>() { { "length", 100 } };
            var shape = CreateShapeInfo("octagon", information, new List<Coordinate>());
            ShapeCalculator.CalculateShape(shape);
            Assert.IsTrue(shape.ShapeVertices.Count == 9);
            ValidateCoordinate(shape.ShapeVertices[0], 130.66, 0);
            ValidateCoordinate(shape.ShapeVertices[1], 92.39, 92.39);
            ValidateCoordinate(shape.ShapeVertices[2], 0, 130.66);
            ValidateCoordinate(shape.ShapeVertices[3], -92.39, 92.39);
            ValidateCoordinate(shape.ShapeVertices[4], -130.66, 0);
            ValidateCoordinate(shape.ShapeVertices[5], -92.39, -92.39);
            ValidateCoordinate(shape.ShapeVertices[6], 0, -130.66);
            ValidateCoordinate(shape.ShapeVertices[7], 92.39, -92.39);
            ValidateCoordinate(shape.ShapeVertices[8], 130.66, 0);
        }

        [TestMethod]
        public void CalculatOctagon_2()
        {
            var information = new Dictionary<string, int>() { { "length", 150 } };
            var shape = CreateShapeInfo("octagon", information, new List<Coordinate>());
            ShapeCalculator.CalculateShape(shape);
            Assert.IsTrue(shape.ShapeVertices.Count == 9);
            ValidateCoordinate(shape.ShapeVertices[0], 195.98, 0);
            ValidateCoordinate(shape.ShapeVertices[1], 138.58, 138.58);
            ValidateCoordinate(shape.ShapeVertices[2], 0, 195.98);
            ValidateCoordinate(shape.ShapeVertices[3], -138.58, 138.58);
            ValidateCoordinate(shape.ShapeVertices[4], -195.98, 0);
            ValidateCoordinate(shape.ShapeVertices[5], -138.58, -138.58);
            ValidateCoordinate(shape.ShapeVertices[6], 0, -195.98);
            ValidateCoordinate(shape.ShapeVertices[7], 138.58, -138.58);
            ValidateCoordinate(shape.ShapeVertices[8], 195.98, 0);
        }
    }
}