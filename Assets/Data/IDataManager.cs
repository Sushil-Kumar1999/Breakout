namespace Assets.Data
{
    public interface IDataManager<T> where T : IData
    {
        void Save(T entity);
        T Load();
    }
}
