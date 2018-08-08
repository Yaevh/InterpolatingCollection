using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TM.Collections
{
    public static class InterpolatingFunctions
    {
        public static InterpolatingFunction<double> LinearDouble => (double from, double to, double factor) =>
        {
            var diff = to - from;
            return from + diff * factor;
        };

        public static InterpolatingFunction<decimal> LinearDecimal => (decimal from, decimal to, double factor) =>
        {
            var diff = to - from;
            return from + diff * (decimal)factor;
        };

        public static InterpolatingFunction<Color> LinearColor => (Color from, Color to, double factor) =>
        {
            var a = LinearDouble(from.A, to.A, factor);
            var r = LinearDouble(from.R, to.R, factor);
            var g = LinearDouble(from.G, to.G, factor);
            var b = LinearDouble(from.B, to.B, factor);

            return Color.FromArgb((int)a, (int)r, (int)g, (int)b);
        };
    }
}
