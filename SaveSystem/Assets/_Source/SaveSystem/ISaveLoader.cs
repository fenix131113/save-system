namespace SaveSystem
{
    public interface ISaveLoader
    {
        void Save<T>(T data, string key = null);
        bool TryLoad<T>(out T result, string key = null);
    }
}