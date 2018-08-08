using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.Collections;

namespace InterpolatingCollection.Tests.GivenEmptyCollection
{
    [TestClass]
    public class CreateCollectionTests
    {
        [TestMethod]
        public void WhenIAddElementsEACBDF_CollectionShouldBeABCDEF()
        {
            var collection = new InterpolatingCollection<string>()
            {
                new InterpolationNode<string>(5.0, "e"),
                new InterpolationNode<string>(1.0, "a"),
                new InterpolationNode<string>(3.0, "c"),
                new InterpolationNode<string>(2.0, "b"),
                new InterpolationNode<string>(4.0, "d"),
                new InterpolationNode<string>(6.0, "f"),
            };
            
            Assert.AreEqual(string.Concat(collection.Select(x => x.Data)), "abcdef");
        }

        [TestMethod]
        public void CollectionWithManyElements_ShouldBeProperlySorted()
        {
            var collection = new InterpolatingCollection<object>();
            var random = new Random();

            for (int i = 0; i < 10000; ++i)
            {
                collection.Add(new InterpolationNode<object>(random.NextDouble(), new object()));
            }

            var rawPoints = collection.Select(x => x.Point).ToArray();
            var sortedPoints = rawPoints.OrderBy(x => x).ToArray();

            Assert.IsTrue(rawPoints.SequenceEqual(sortedPoints));
        }

        [TestMethod]
        public void WhenAddingAlreadyExistingPoint_SouldThrowInvalidOperationException()
        {
            var collection = new InterpolatingCollection<string>()
            {
                new InterpolationNode<string>(5.0, "e"),
                new InterpolationNode<string>(1.0, "a"),
                new InterpolationNode<string>(3.0, "c"),
                new InterpolationNode<string>(2.0, "b"),
                new InterpolationNode<string>(4.0, "d"),
                new InterpolationNode<string>(6.0, "f"),
            };

            Assert.ThrowsException<InvalidOperationException>(() => collection.Add(new InterpolationNode<string>(2.0, string.Empty)));
        }
    }
}
