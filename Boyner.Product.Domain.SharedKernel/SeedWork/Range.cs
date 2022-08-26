using System;

namespace Boyner.Product.Domain.SharedKernel.SeedWork
{
    public class Range<T> where T : IComparable
    {
        public T Min { get { return min; } }
        public T Max { get { return max; } }

        readonly T min;
        readonly T max;

        public Range(T min, T max)
        {
            this.min = min;
            this.max = max;
        }

        /// <summary>
        /// Checking overlap.
        /// </summary>
        /// <param name="other">Other instance</param>
        /// <returns></returns>
        public bool IsOverlapped(Range<T> other)
        {
            return Min.CompareTo(other.Max) < 0 && other.Min.CompareTo(Max) < 0;
        }

        /// <summary>
        /// Checking overlap with include boundaries.
        /// </summary>
        /// <param name="other">Other instance</param>
        /// <returns></returns>
        public bool Includes(Range<T> other)
        {
            return Min.CompareTo(other.Max) <= 0 && other.Min.CompareTo(Max) <= 0;
        }

        /// <summary>
        /// Checking desired value include in range
        /// </summary>
        /// <param name="value">Checking value</param>
        /// <returns></returns>
        public bool Includes(T value) => Min.CompareTo(value) <= 0 && Max.CompareTo(value) >= 0;
    }
}
