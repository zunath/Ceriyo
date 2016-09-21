namespace Ceriyo.Core.Contracts
{
    public interface IDataService
    {
        T Load<T>(string filePath = null)
            where T: class;

        void Save<T>(T obj, string filePath = null)
            where T : class;
    }
}
