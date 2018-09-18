# InterpolatingCollection
A .NET collection of nodes and their values, allowing interpolation between those values.

# Example
```csharp
// define our interpolation (or use on of the standard ones from InterpolatingFunctions class)
InterpolatingFunction<Color> linearColorInterpolation = (Color from, Color to, double factor) =>
{
    var a = InterpolatingFunctions.LinearDouble(from.A, to.A, factor);
    var r = InterpolatingFunctions.LinearDouble(from.R, to.R, factor);
    var g = InterpolatingFunctions.LinearDouble(from.G, to.G, factor);
    var b = InterpolatingFunctions.LinearDouble(from.B, to.B, factor);

    return Color.FromArgb((int)a, (int)r, (int)g, (int)b);
};

// create a collection and add some points
var collection = new InterpolatingCollection<Color>(linearColorInterpolation);
collection[0.0] = Color.Black;
collection[100.0] = Color.White;

// retrieve interpolated values
var black = collection[0.0];
var darkGray = collection[25.0];
var gray = collection[50.0];
var lightGray = collection[75.0];
var white = collection[100.0];
```
