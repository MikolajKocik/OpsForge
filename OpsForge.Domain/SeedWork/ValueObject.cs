using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsForge.Domain.SeedWork
{
    public abstract class ValueObject
    {
        /// <summary>
        /// Determines wheter two instances <see cref="ValueObject"/> are equal.
        /// </summary>
        /// <param name="left">Pierwszy <see cref="ValueObject"/> do porównania.</param>
        /// <param name="right">Drugi <see cref="ValueObject"/> do porównania.</param>
        /// <returns><see langword="true"/>, jeśli określone <paramref name="left"/> i <paramref name="right"/> są równe;
        /// w przeciwnym razie <see langword="false"/>.</returns>
        protected static bool EqualOperator(ValueObject left, ValueObject right)
        {
            if (ReferenceEquals(left, null))
                return false;

            return ReferenceEquals(left, right) || left.Equals(right);
        }

        /// <summary>
        /// Determines whether two <see cref="ValueObject"/> instances are not equal.
        /// </summary>
        /// <param name="left">The first <see cref="ValueObject"/> to compare.</param>
        /// <param name="right">The second <see cref="ValueObject"/> to compare.</param>
        /// <returns><see langword="true"/> if the specified <paramref name="left"/> and <paramref name="right"/> are not equal;
        /// otherwise, <see langword="false"/>.</returns>
        protected static bool NotEqualOperator(ValueObject left, ValueObject right)
        {
            return !(EqualOperator(left, right));
        }

        /// <summary>
        /// Returns the atomic values that are used to determine equality for the current object.
        /// </summary>
        /// <remarks>Override this method to provide the sequence of values that uniquely identify the
        /// object for equality comparisons. The returned components are used to implement value-based equality and hash
        /// code generation.</remarks>
        /// <returns>—An <see cref="IEnumerable{T}"/> of objects representing the values to be used for equality comparison. The
        /// order of the returned components is significant.</returns>
        protected abstract IEnumerable<object> GetEqualityComponents();

        /// <summary>
        /// Determines whether the specified object is equal to the current value object.
        /// </summary>
        /// <remarks>Two value objects are considered equal if they are of the same type and all their
        /// equality components are equal. This method supports value-based equality comparison.</remarks>
        /// <param name="obj">The object to compare with the current value object.</param>
        /// <returns><see langword="true"/> if the specified object is a value object of the same type and its equality
        /// components are equal to those of the current object; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var other = (ValueObject)obj;

            return this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        /// <summary>
        /// Returns a hash code for the current object based on its equality components.
        /// </summary>
        /// <remarks>The hash code is computed by combining the hash codes of the object's equality
        /// components. This method is intended to be consistent with the implementation of <see
        /// cref="object.Equals(object)"/> for value objects.</remarks>
        /// <returns>An integer hash code representing the current object.</returns>
        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y); 
        }
    }
}