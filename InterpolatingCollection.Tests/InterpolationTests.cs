using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TM.Collections;

namespace InterpolatingCollection.Tests
{
    [TestClass]
    public class GivenCollectionOfPointsOfRange1to5AndValuesOfRange10to50WithLinearInterpolation
    {
        [TestMethod]
        public void WhenQueriedForIntermediateValue_ShouldReturnProperInterpolatedValue()
        {
            var collection = BuildCollection();

            Assert.AreEqual(25.0, collection[2.5]);
        }

        [TestMethod]
        public void WhenQueriedForExactValue_ShouldReturnNodeValue()
        {
            var collection = BuildCollection();

            Assert.AreEqual(20.0, collection[2.0]);
        }

        [TestMethod]
        public void WhenQueriedForFirstValue_ShouldReturnNodeValue()
        {
            var collection = BuildCollection();

            Assert.AreEqual(10.0, collection[1.0]);
        }

        [TestMethod]
        public void WhenQueriedForLastValue_ShouldReturnNodeValue()
        {
            var collection = BuildCollection();

            Assert.AreEqual(50.0, collection[5.0]);
        }

        [TestMethod]
        public void WhenQueriedForPointBelowLowerBound_ShouldThrowOutOfRangesException()
        {
            var collection = BuildCollection();

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => collection[0.5]);
        }

        [TestMethod]
        public void WhenQueriedForPointAboveUpperBound_ShouldThrowOutOfRangesException()
        {
            var collection = BuildCollection();

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => collection[6]);
        }

        [TestMethod]
        public void WhenInsertedNewNodeAtEnd_ShouldReturnProperInterpolatedValue()
        {
            var collection = BuildCollection();
            collection[6.0] = 100.0;

            Assert.AreEqual(75.0, collection[5.5]);
        }

        [TestMethod]
        public void WhenInsertedNewNodeInTheMiddle_ShouldReturnProperInterpolatedValue()
        {
            var collection = BuildCollection();
            collection[2.5] = 100.0;

            Assert.AreEqual(60.0, collection[2.25]);
        }

        private InterpolatingCollection<double> BuildCollection()
        {
            return new InterpolatingCollection<double>(DoubleInterpolation)
            {
                new InterpolationNode<double>(2.0, 20.0),
                new InterpolationNode<double>(5.0, 50.0),
                new InterpolationNode<double>(1.0, 10.0),
                new InterpolationNode<double>(3.0, 30.0),
                new InterpolationNode<double>(4.0, 40.0),
            };
        }

        private double DoubleInterpolation(double from, double to, double factor)
        {
            var diff = to - from;
            return from + diff * factor;
        }
    }
}
