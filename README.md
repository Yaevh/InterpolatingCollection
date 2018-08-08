# InterpolatingCollection
A .NET collection of nodes and their values, allowing interpolation between those values.

# Example
```csharp
var linearColorInterpolation = (Color from, Color to, double factor) =>
{
    var a = InterpolatingFunctions.LinearDouble(from.A, to.A, factor);
    var r = InterpolatingFunctions.LinearDouble(from.R, to.R, factor);
    var g = InterpolatingFunctions.LinearDouble(from.G, to.G, factor);
    var b = InterpolatingFunctions.LinearDouble(from.B, to.B, factor);

    return Color.FromArgb((int)a, (int)r, (int)g, (int)b);
};

var collection = new InterpolatingCollection<Color>(linearColorInterpolation)
{
    new InterpolationNode<Color>(0.0, Color.Black),
    new InterpolationNode<Color>(100.0, Color.White)
};

var lightGray = collection[25.0];
var middleGray = collection[50.0];
var darkGray = collection[75.0];
```
