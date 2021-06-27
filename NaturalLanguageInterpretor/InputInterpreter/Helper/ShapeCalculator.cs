using InputInterpreter.Models;
using System;
using System.Collections.Generic;

namespace InputInterpreter.Helper
{
    public class ShapeCalculator
    {
        public static void CalculateShape(ShapeInfo shapeInfo)
        {
            switch (shapeInfo.Shape)
            {
                case "isosceles triangle":
                    CalculateIsoscelesTriangle(shapeInfo);
                    break;
                case "square":
                    CalculateBox(shapeInfo);
                    break;
                case "scalene triangle":
                    CalculateScaleneTriangle(shapeInfo);
                    break;
                case "parallelogram":
                    CalculateParallelogram(shapeInfo);
                    break;
                case "equilateral triangle":
                    CalculatEquilateralTriangle(shapeInfo);
                    break;
                case "pentagon":
                    CalculatePolygonPoints(shapeInfo, 5);
                    break;
                case "rectangle":
                    CalculateBox(shapeInfo);
                    break;
                case "hexagon":
                    CalculatePolygonPoints(shapeInfo, 6);
                    break;
                case "heptagon":
                    CalculatePolygonPoints(shapeInfo, 7);
                    break;
                case "octagon":
                    CalculatePolygonPoints(shapeInfo, 8);
                    break;
                case "circle":
                    break;
                case "oval":
                    break;
                default:
                    throw new ArgumentException($"Invalid Shape [{shapeInfo.Shape}] specified");
            }
        }

        private static void CalculatEquilateralTriangle(ShapeInfo shapeInfo)
        {
            var length = shapeInfo.Information["length"];

            var startx = -(length / 2);
            var starty = -(0.375 * length);
            var height = 0.75 * length;
            shapeInfo.ShapeVertices = new List<Coordinate>();
            shapeInfo.ShapeVertices.Add(new Coordinate(startx, starty));
            shapeInfo.ShapeVertices.Add(new Coordinate(startx + length, starty));
            shapeInfo.ShapeVertices.Add(new Coordinate(startx + length / 2, starty + height));
            shapeInfo.ShapeVertices.Add(new Coordinate(startx, starty));
        }

        private static void CalculateParallelogram(ShapeInfo shapeInfo)
        {
            var lengtha = shapeInfo.Information["lengtha"];
            var lengthb = shapeInfo.Information["lengthb"];

            var x = (double)Math.Sqrt(Math.Pow(lengthb, 2) / 2);
            var startx = -(lengtha/2);
            var starty = -(x / 2);
            
            shapeInfo.ShapeVertices = new List<Coordinate>();
            shapeInfo.ShapeVertices.Add(new Coordinate(startx, starty));
            shapeInfo.ShapeVertices.Add(new Coordinate(startx + lengtha, starty));
            shapeInfo.ShapeVertices.Add(new Coordinate(startx + lengtha + x, starty + x));
            shapeInfo.ShapeVertices.Add(new Coordinate(startx + x, starty + x));
            shapeInfo.ShapeVertices.Add(new Coordinate(startx, starty));
        }

        private static void CalculateBox(ShapeInfo shapeInfo)
        {
            int width, height;
            
            // Check square or rectangle
            if (shapeInfo.Information.TryGetValue("length", out var length))
            {
                width = length;
                height = length;
            }
            else
            {
                width = shapeInfo.Information["width"];
                height = shapeInfo.Information["height"];
            }

            var startx = -(width / 2);
            var starty = -(height / 2);
            shapeInfo.ShapeVertices = new List<Coordinate>();
            shapeInfo.ShapeVertices.Add(new Coordinate(startx, starty));
            shapeInfo.ShapeVertices.Add(new Coordinate(startx + width, starty));
            shapeInfo.ShapeVertices.Add(new Coordinate(startx + width, starty + height));
            shapeInfo.ShapeVertices.Add(new Coordinate(startx, starty + height));
            shapeInfo.ShapeVertices.Add(new Coordinate(startx, starty));
        }

        private static void CalculateIsoscelesTriangle(ShapeInfo shapeInfo)
        {
            var width = shapeInfo.Information["width"];
            var height = shapeInfo.Information["height"];

            var startx = -(width / 2);
            var starty = -(height / 2);

            shapeInfo.ShapeVertices = new List<Coordinate>();
            shapeInfo.ShapeVertices.Add(new Coordinate(startx, starty));
            shapeInfo.ShapeVertices.Add(new Coordinate(startx + width, starty));
            shapeInfo.ShapeVertices.Add(new Coordinate(startx + width / 2, starty + height));
            shapeInfo.ShapeVertices.Add(new Coordinate(startx, starty));
        }

        private static void CalculateScaleneTriangle(ShapeInfo shapeInfo)
        {
            var sideAB = shapeInfo.Information["lengtha"]; // this will be the base length
            var sideBC = shapeInfo.Information["lengthb"];
            var sideAC = shapeInfo.Information["lengthc"];

            var x = (Math.Pow(sideAB, 2) + Math.Pow(sideAC, 2) - Math.Pow(sideBC,2)) / (2 * sideAB);
            var y = Math.Sqrt(Math.Abs(Math.Pow(sideAC, 2) - Math.Pow(x, 2)));

            var startx =  -(sideAB / 2);
            var starty = -(y / 2);

            shapeInfo.ShapeVertices = new List<Coordinate>();
            shapeInfo.ShapeVertices.Add(new Coordinate(startx, starty));
            shapeInfo.ShapeVertices.Add(new Coordinate(startx + sideAB, starty));
            shapeInfo.ShapeVertices.Add(new Coordinate(startx + x, starty + y));
            shapeInfo.ShapeVertices.Add(new Coordinate(startx, starty));
        }

        private static void CalculatePolygonPoints(ShapeInfo shapeInfo, int sideCount)
        {
            var sideLength = shapeInfo.Information["length"];
            shapeInfo.ShapeVertices = new List<Coordinate>();
            var radius = (double)(sideLength / (2 * Math.Sin((Math.PI / sideCount))));
            for (int i = 0; i <= sideCount; i++)
            {
                shapeInfo.ShapeVertices.Add(
                    new Coordinate(
                        radius * (Math.Cos((2 * Math.PI * i) / sideCount)), 
                        radius * (Math.Sin((2 * Math.PI * i) / sideCount))
                    )
                );
            }
        }
                
    } 
}
