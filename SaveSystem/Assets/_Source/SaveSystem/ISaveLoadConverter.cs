namespace SaveSystem
{
    public interface ISaveLoadConverter
    {
        bool CanConvert(System.Type type);
        
        public string Serialize(object obj);
        public object Deserialize(string obj);
    }
}