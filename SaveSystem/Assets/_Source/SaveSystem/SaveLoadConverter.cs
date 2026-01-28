namespace SaveSystem
{
    public abstract class SaveLoadConverter<T> : ISaveLoadConverter
    {
        public bool CanConvert(System.Type type) => type == typeof(T);

        public string Serialize(object obj)
            => SerializeTyped((T)obj);

        public object Deserialize(string data)
            => DeserializeTyped(data);

        protected abstract string SerializeTyped(T obj);
        protected abstract T DeserializeTyped(string data);
    }
}