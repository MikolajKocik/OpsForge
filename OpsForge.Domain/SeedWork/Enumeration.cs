using System.Reflection;

namespace OpsForge.Domain.SeedWork;

public abstract class Enumeration : IComparable
{
    public string Name { get; private set; }

    public int Id { get; private set; }

    protected Enumeration(int id, string name) => (this.Id, this.Name) = (id, name);

    public override string ToString() => this.Name;

    /// <summary>
    /// Returns all defined instances of the specified <see cref="Enumeration"/> type.
    /// </summary>
    /// <remarks>This method is typically used to enumerate all possible values of a custom enumeration class
    /// that follows the pattern of defining named instances as public static fields.</remarks>
    /// <typeparam name="T">The type of <see cref="Enumeration"/> to retrieve instances for. Must be a class derived from <see
    /// cref="Enumeration"/>.</typeparam>
    /// <returns>An <see cref="IEnumerable{T}"/> containing all public static fields declared on <typeparamref name="T"/> that
    /// represent instances of the enumeration type.</returns>
    public static IEnumerable<T> GetAll<T>() where T : Enumeration =>
        typeof(T).GetFields(BindingFlags.Public |
                            BindingFlags.Static |
                            BindingFlags.DeclaredOnly)
                 .Select(f => f.GetValue(null))
                 .Cast<T>();

    /// <summary>
    /// Determines whether the specified object is equal to the current <see cref="Enumeration"/> instance.
    /// </summary>
    /// <param name="obj">The object to compare with the current instance.</param>
    /// <returns><see langword="true"/> if the specified object is an <see cref="Enumeration"/> of the same type and has the same
    /// identifier; otherwise, <see langword="false"/>.</returns>
    public override bool Equals(object obj)
    {
        if (obj is not Enumeration otherValue)
        {
            return false;
        }

        bool typeMatches = GetType().Equals(obj.GetType());
        bool valueMatches = Id.Equals(otherValue.Id);

        return typeMatches && valueMatches;
    }

    /// <summary>
    /// Compares the current instance with another object of the same type and returns an integer that indicates whether
    /// the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
    /// </summary>
    /// <param name="other">An object to compare with this instance. Must be of type <see cref="Enumeration"/>.</param>
    /// <returns>A value less than zero if this instance precedes <paramref name="other"/> in the sort order; zero if this
    /// instance occurs in the same position as <paramref name="other"/>; greater than zero if this instance follows
    /// <paramref name="other"/> in the sort order.</returns>
    public int CompareTo(object other) => this.Id.CompareTo(((Enumeration)other).Id);

    public static T FromValue<T>(int value) where T : Enumeration, new()
    {
        var matchingItem = parse<T, int>(value, "value", item => item.Id == value);
        return matchingItem;
    }

    /// <summary>
    /// Returns an instance of the specified <see cref="Enumeration"/> type that matches the provided name.
    /// </summary>
    /// <remarks>Use this method to retrieve a strongly typed <see cref="Enumeration"/> instance by its name.
    /// If no matching value exists, the method returns <c>null</c>.</remarks>
    /// <typeparam name="T">The type of <see cref="Enumeration"/> to search. Must have a parameterless constructor.</typeparam>
    /// <param name="name">The name of the enumeration value to retrieve. The comparison is typically case-sensitive.</param>
    /// <returns>An instance of <typeparamref name="T"/> whose <c>Name</c> property matches <paramref name="name"/>; or
    /// <c>null</c> if no match is found.</returns>
    public static T FromName<T>(string name) where T : Enumeration, new()
    {
        T? matchingItem = parse<T, string>(name, "name", item => item.Name == name);
        return matchingItem;
    }

    /// <summary>
    /// Attempts to find an instance of the specified <see cref="Enumeration"/> type that matches the given predicate.
    /// </summary>
    /// <remarks>This method is typically used to implement parsing logic for custom enumeration types derived
    /// from <see cref="Enumeration"/>.</remarks>
    /// <typeparam name="T">The type of <see cref="Enumeration"/> to search for a matching instance.</typeparam>
    /// <typeparam name="K">The type of the value being parsed.</typeparam>
    /// <param name="value">The original value being parsed, used for error reporting if no match is found.</param>
    /// <param name="description">A description of the value being parsed, included in the exception message if parsing fails.</param>
    /// <param name="predicate">A function that defines the criteria for selecting a matching <typeparamref name="T"/> instance.</param>
    /// <returns>The first <typeparamref name="T"/> instance that satisfies the specified predicate.</returns>
    /// <exception cref="ApplicationException">Thrown if no instance of <typeparamref name="T"/> satisfies the <paramref name="predicate"/>. The exception
    /// message includes the provided <paramref name="value"/> and <paramref name="description"/>.</exception>
    private static T parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration, new()
    {
        T? matchingItem = GetAll<T>().FirstOrDefault(predicate);

        if (matchingItem == null)
        {
            string message = string.Format("'{0}' is not a valid {1} in {2}", value, description, typeof(T));
            throw new ApplicationException(message);
        }

        return matchingItem;
    }
}