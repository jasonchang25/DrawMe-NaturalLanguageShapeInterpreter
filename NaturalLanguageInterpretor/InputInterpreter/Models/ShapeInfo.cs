using System;
using System.Collections.Generic;

namespace InputInterpreter.Models
{
    public class ShapeInfo
    {
        public string Shape { get; set; }
        public Dictionary<string,int> Information { get; set; }
        public List<Coordinate> ShapeVertices { get; set; }
    }

    public class Coordinate
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Coordinate(double x, double y)
        {
            X = Math.Round(x,2);
            Y = Math.Round(y,2);
        }
    }
}