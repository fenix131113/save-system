using System;
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
            if (_saveLoader.TryLoad(out TimerModel model, TIME_SAVE_KEY))
                _timer = model.time;
        }

        public void Save()
        {
            _saveLoader.Save(new TimerModel(_timer + Time.time), TIME_SAVE_KEY);
        }

        [Serializable]
        private class TimerModel
        {
            public float time;

            public TimerModel(float time) => this.time = time;
        }
    }
}