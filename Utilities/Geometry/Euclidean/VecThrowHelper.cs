namespace Utilities.Geometry.Euclidean;

/// <summary>
///     Helper methods for throwing common Vector exceptions.
/// </summary>
public static class VecThrowHelper
{
    /// <summary>
    ///     Throws an <see cref="ArgumentException"/> for invalid component access.
    /// </summary>
    /// <typeparam name="TVec">The vector type that was accessed</typeparam>
    /// <param name="component">The invalid component that was accessed</param>
    public static ArgumentException InvalidComponent<TVec>(Axis component)
    {
        return new ArgumentException($"The {component} component does not exist in {typeof(TVec).Name} space");
    }

    /// <summary>
    ///     Throws an <see cref="ArgumentException"/> for an unsupported distance metric.
    /// </summary>
    /// <typeparam name="TVec">The vector type the metric was applied to</typeparam>
    /// <param name="metric">The unsupported metric</param>
    public static ArgumentException InvalidMetric<TVec>(Metric metric)
    {
        return new ArgumentException($"The {metric} distance metric is not well defined over {typeof(TVec).Name} space");
    }
}