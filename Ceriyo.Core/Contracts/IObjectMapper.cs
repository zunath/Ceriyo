namespace Ceriyo.Core.Contracts
{
    public interface IObjectMapper
    {
        /// <summary>
        /// Initializes all mappings. Should be called only once on application load.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Maps a source object to a new destination object.
        /// </summary>
        /// <typeparam name="TDestination">The type of the new object.</typeparam>
        /// <param name="source">The object to map from.</param>
        /// <returns>The newly created object which was mapped to.</returns>
        TDestination Map<TDestination>(object source);

        /// <summary>
        /// Maps a source object to an existing destination object.
        /// </summary>
        /// <typeparam name="TSource">Source type to use</typeparam>
        /// <typeparam name="TDestination">Destination type</typeparam>
        /// <param name="source">Source object to map from.</param>
        /// <param name="destination">Destination object to map to.</param>
        /// <returns>The mapped destinatino object, same instance as <paramref name="destination"/></returns>
        TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
    }
}
