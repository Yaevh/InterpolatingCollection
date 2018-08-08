using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TM.Collections
{
    /// <summary>
    /// Represents a collection of nodes and their values, allowing interpolation between those values
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class InterpolatingCollection<T> : ICollection<InterpolationNode<T>>
    {
        private List<InterpolationNode<T>> _collection = new List<InterpolationNode<T>>();

        /// <summary>
        /// Function that interpolates the result value between two input values
        /// </summary>
        public InterpolatingFunction<T> Interpolation { get; set; }


        public InterpolatingCollection() { }

        public InterpolatingCollection(InterpolatingFunction<T> interpolation) : this(null, interpolation) { }

        public InterpolatingCollection(IEnumerable<InterpolationNode<T>> source, InterpolatingFunction<T> interpolation)
        {
            Interpolation = interpolation ?? throw new ArgumentNullException(nameof(interpolation));

            if (source != null)
                foreach (var item in source)
                    Add(item);
        }


        #region basic ICollection implementation

        public int Count => _collection.Count;

        public bool IsReadOnly => (_collection as ICollection<InterpolationNode<T>>).IsReadOnly;
        
        public bool Contains(InterpolationNode<T> item)
        {
            return _collection.Contains(item);
        }

        public void CopyTo(InterpolationNode<T>[] array, int arrayIndex)
        {
            _collection.CopyTo(array, arrayIndex);
        }

        public IEnumerator<InterpolationNode<T>> GetEnumerator()
        {
            return ((ICollection<InterpolationNode<T>>)_collection).GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((ICollection<InterpolationNode<T>>)_collection).GetEnumerator();
        }

        #endregion


        #region items manipulation

        /// <summary>
        /// Adds <paramref name="node"/> to the <see cref="InterpolatingCollection{T}"/> at a proper position
        /// </summary>
        /// <param name="node"><see cref="InterpolationNode{T}"/> to add to the collection</param>
        /// <exception cref="InvalidOperationException">The <see cref="InterpolatingCollection{T}"/> already contains a node with this <see cref="InterpolationNode{T}.Point"/></exception>
        public void Add(InterpolationNode<T> node)
        {
            var insertionIndex = _collection.BinarySearch(node, new InterpolationNodeComparer<T>());
            if (insertionIndex >= 0)
                throw new InvalidOperationException($"Collection already contains {nameof(node)} with {nameof(node.Point)} of this value");
            if (insertionIndex < 0)
                insertionIndex = ~insertionIndex;
            _collection.Insert(insertionIndex, node);
        }

        /// <summary>
        /// Removes a specific <paramref name="node"/> from this <see cref="InterpolatingCollection{T}"/>
        /// </summary>
        /// <param name="node"></param>
        /// <returns>True if this this <see cref="InterpolatingCollection{T}"/> contained <paramref name="node"/> and it was successfully removed; false otherwise</returns>
        public bool Remove(InterpolationNode<T> node)
        {
            return _collection.Remove(node);
        }

        /// <summary>
        /// Removes all items from this <see cref="InterpolatingCollection{T}"/>
        /// </summary>
        public void Clear()
        {
            _collection.Clear();
        }

        #endregion

        /// <summary>
        /// Gets or sets the <see cref="InterpolationNode{T}.Data"/> with the specified value of <see cref="InterpolationNode{T}.Point"/>.
        /// If used to set an element, a new <see cref="InterpolationNode{T}"/> is added at the position of <paramref name="point"/>.
        /// </summary>
        /// <param name="point">Value of <see cref="InterpolationNode{T}.Point"/> to get or set</param>
        /// <returns>
        /// The <see cref="InterpolationNode{T}.Data"/> of an element with the specified value of <see cref="InterpolationNode{T}.Point"/>.
        /// If such an element exists in the <see cref="InterpolatingCollection{T}"/>, it is returned.
        /// Otherwise, two closest elements are selected and interpolated using <see cref="InterpolatingCollection{T}.Interpolation"/>.
        /// </returns>
        public T this[double point]
        {
            get
            {
                if (Interpolation == null)
                    throw new InvalidOperationException($"{nameof(Interpolation)} must be set before retrieving interpolated value");

                var node = new InterpolationNode<T>(point, default(T));
                var index = _collection.BinarySearch(node, new InterpolationNodeComparer<T>());
                if (index < 0)
                    index = ~index;

                if (index >= _collection.Count)
                    throw new ArgumentOutOfRangeException(nameof(point));
                if (index == 0 && _collection[index].Point == point)
                    return _collection[index].Data;

                if (index == 0)
                    throw new ArgumentOutOfRangeException(nameof(point));

                var previous = _collection[index - 1];
                var next = _collection[index];

                var interpolationFactor = (point - previous.Point) / (next.Point - previous.Point);
                return Interpolation.Invoke(previous.Data, next.Data, interpolationFactor);
            }
            set
            {
                Add(new InterpolationNode<T>(point, value));
            }
        }
        
    }
}
