using System;
using System.Collections.Generic;
using System.Text;

namespace TM.Collections
{
    public class InterpolationNode<T>
    {
        public double Point { get; }

        public T Data { get; }

        public InterpolationNode(double point, T data)
        {
            Point = point;
            Data = data;
        }

        public override string ToString() => $"[{Point:0.00}]: {Data}";
    }
}
