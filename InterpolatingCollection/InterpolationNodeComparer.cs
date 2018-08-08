using System;
using System.Collections.Generic;
using System.Text;

namespace TM.Collections
{
    internal class InterpolationNodeComparer<T> : IComparer<InterpolationNode<T>>
    {
        public int Compare(InterpolationNode<T> x, InterpolationNode<T> y)
        {
            if (x == null || y == null)
                return Comparer<InterpolationNode<T>>.Default.Compare(x, y);

            return Comparer<double>.Default.Compare(x.Point, y.Point);
        }
    }
}
