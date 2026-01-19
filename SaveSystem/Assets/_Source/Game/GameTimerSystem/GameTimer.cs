using Game.Core.Saves;
using SaveSystem;
using UnityEngine;

namespace Game.GameTimerSystem
{
    public class GameTimer : MonoBehaviour, ILoadSavable
    {
        private const string TIME_SAVE_KEY = "game_time";
        
        private float _timer;
        private ISaveLoader _saveLoader;

        public void Construct(ISaveLoader saveLoader)
        {
            _saveLoader = saveLoader;
        }

        public float GetTimer() => _timer + Time.time;
        
        private void OnApplicationQuit()
        {
            Save();
        }

        public void Load()
        {
            _saveLoader.TryLoad(out _timer, TIME_SAVE_KEY);
        }

        public void Save()
        {
            _saveLoader.Save(_timer + Time.time, TIME_SAVE_KEY);
        }
    }
}