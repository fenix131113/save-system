using Game.Core.Saves;

namespace Game.Core
{
    public class GameSaveLoader : ILoadSavable
    {
        private readonly ILoadSavable[] _loadSavable;
        
        public GameSaveLoader(params ILoadSavable[] loadSavable) => _loadSavable = loadSavable;
        
        public void Load()
        {
            foreach (var loadSavable in _loadSavable)
                loadSavable.Load();
        }

        public void Save()
        {
            foreach (var loadSavable in _loadSavable)
                loadSavable.Save();
        }
    }
}