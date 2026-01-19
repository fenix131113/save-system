namespace Game.Core.Saves
{
    public interface ILoadSavable
    {
        void Load();
        void Save();
    }
}