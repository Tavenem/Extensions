namespace Tavenem;

/// <summary>
/// Indicates that a <see cref="IFloatingPoint{TSelf}"/> type can be compared to zero
/// by using a near-equivalence value indicated by the <see cref="NearlyZero"/> property.
/// </summary>
public interface IComparableToZero<TSelf> : IFloatingPoint<TSelf> where TSelf : IComparableToZero<TSelf>
{
    /// <summary>
    /// A value which can be used to determine near-equivalence to zero,
    /// or to other values of the same type.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This is not usually the same as <see cref="IFloatingPoint{TSelf}.Epsilon"/>,
    /// which is the smallest possible value that is distinguishable from zero.
    /// </para>
    /// <para>
    /// The <see cref="NearlyZero"/> value is instead used when making comparisons
    /// to determine close equivalence, and thereby avoid false negative comparison
    /// results due to floating-point errors.
    /// </para>
    /// </remarks>
    public TSelf NearlyZero { get; }
}
