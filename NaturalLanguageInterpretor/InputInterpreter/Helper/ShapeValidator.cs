using InputInterpreter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InputInterpreter.Helper
{
    public class ShapeValidator
    {
        public static void ValidateShape(ShapeInfo shapeInfo)
        {
            switch (shapeInfo.Shape)
            {
                case "isosceles triangle":
                    ValidateWidthAndHeightShapes(shapeInfo);
                    // I really should put a check to make sure the height specified is valid so it doesn't end up making a equilaterial triangle, but since inputs can only be integers it's quite rare
                    break;
                case "square":
                    ValidateSingleLengthShapes(shapeInfo);
                    break;
                case "scalene triangle":
                    ValidateScaleneTriangle(shapeInfo);
                    break;
                case "parallelogram":
                    ValidateParallelogram(shapeInfo);
                    break;
                case "equilateral triangle":
                    ValidateSingleLengthShapes(shapeInfo);
                    break;
                case "pentagon":
                    ValidateSingleLengthShapes(shapeInfo);
                    break;
                case "rectangle":
                    ValidateWidthAndHeightShapes(shapeInfo);
                    if (shapeInfo.Information["width"] == shapeInfo.Information["height"])
                        throw new ArgumentException($"Invalid measurement(s) provided for shape [rectangle]. width and height amount can not be the same.");
                    break;
                case "hexagon":
                    ValidateSingleLengthShapes(shapeInfo);
                    break;
                case "heptagon":
                    ValidateSingleLengthShapes(shapeInfo);
                    break;
                case "octagon":
                    ValidateSingleLengthShapes(shapeInfo);
                    break;
                case "circle":
                    ValidateCircle(shapeInfo);
                    break;
                case "oval":
                    ValidateWidthAndHeightShapes(shapeInfo);
                    break;
                default:
                    throw new ArgumentException($"Invalid Shape [{shapeInfo.Shape}] specified");
            }
        }

        private static void ValidateCircle(ShapeInfo shapeInfo)
        {
            InvalidMeasurementCountCheck(shapeInfo, 1, "radius");
            InvalidMeasurementCheck("radius", shapeInfo);
            InvalidAmountCheck("radius", shapeInfo.Information["radius"]);
        }

        private static void ValidateParallelogram(ShapeInfo shapeInfo)
        {
            InvalidMeasurementCountCheck(shapeInfo, 2, "2 measurement lengtha and lengthb");
            InvalidMeasurementCheck("lengtha", shapeInfo);
            InvalidMeasurementCheck("lengthb", shapeInfo);
            var a = shapeInfo.Information["lengtha"];
            var b = shapeInfo.Information["lengthb"];
            InvalidAmountCheck("lengtha", a);
            InvalidAmountCheck("lengthb", b);
        }

        private static void ValidateScaleneTriangle(ShapeInfo shapeInfo)
        {
            InvalidMeasurementCountCheck(shapeInfo, 3, "3 measurement length [a,b,c]"); 
            InvalidMeasurementCheck("lengtha", shapeInfo);
            InvalidMeasurementCheck("lengthb", shapeInfo);
            InvalidMeasurementCheck("lengthc", shapeInfo);

            var a = shapeInfo.Information["lengtha"];
            var b = shapeInfo.Information["lengthb"];
            var c = shapeInfo.Information["lengthc"];
            InvalidAmountCheck("lengtha", a);
            InvalidAmountCheck("lengthb", b);
            InvalidAmountCheck("lengthc", c);

            // check valid scalene triangle based on lengths
            if (new HashSet<int> { a,b,c}.Count != 3)
            {
                throw new ArgumentException($"Invalid lengths provided for shape [Scalene Triangle]. All side lengths must be different");
            }

            if (a + b <= c || a + c <= b || b + c <= a)
                throw new ArgumentException($"Invalid lengths provided for shape [Scalene Triangle]. Impossible to make a closed triangle with lengths [{a}, {b}, {c}] provided");
        }

        private static void ValidateSingleLengthShapes(ShapeInfo shapeInfo)
        {
            InvalidMeasurementCountCheck(shapeInfo, 1, "1");
            InvalidMeasurementCheck("length", shapeInfo, "side length");
            InvalidAmountCheck("length", shapeInfo.Information["length"], "side length");
        }

        private static void ValidateWidthAndHeightShapes(ShapeInfo shapeInfo)
        {
            InvalidMeasurementCountCheck(shapeInfo, 2, "width and height");
            InvalidMeasurementCheck("width", shapeInfo);
            InvalidMeasurementCheck("height", shapeInfo);
            InvalidAmountCheck("width", shapeInfo.Information["width"]);
            InvalidAmountCheck("height", shapeInfo.Information["height"]);
        } 
        
        private static void InvalidMeasurementCheck(string measurmentRequired, ShapeInfo shapeInfo, string measurementText = null)
        {
            if (!shapeInfo.Information.ContainsKey(measurmentRequired))
            {
                throw new ArgumentException($"Invalid measurement(s) provided for shape [{shapeInfo.Shape}]. {measurementText ?? measurmentRequired} must be provided");
            }
        }

        private static void InvalidAmountCheck(string measurement, int amount, string measurementText = null)
        {
            if (amount < 1)
            {
                throw new ArgumentException($"Invalid {measurement} amount provided. Value must be greater than 0");
            }
        }

        private static void InvalidMeasurementCountCheck(ShapeInfo shapeInfo, int measurementCount, string measurementText)
        {
            if (shapeInfo.Information.Count != measurementCount)
            {
                throw new ArgumentException($"Invalid number of measurement(s) provided for shape [{shapeInfo.Shape}]. Only {measurementText} measurement needs to be specified");
            }
        }
    } 
}
