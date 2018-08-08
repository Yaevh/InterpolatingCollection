using System;
using System.Collections.Generic;
using System.Text;

namespace TM.Collections
{
    /// <summary>
    /// Encapsulates a method that calculates a result interpolated between two values with a given <see cref="factor"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="from">Value to interpolate from</param>
    /// <param name="to">Value to interpolate to</param>
    /// <param name="factor">Interpolation factor of range from 0.0 to 1.0</param>
    /// <returns>Interpolated value</returns>
    public delegate T InterpolatingFunction<T>(T from, T to, double factor);
}
