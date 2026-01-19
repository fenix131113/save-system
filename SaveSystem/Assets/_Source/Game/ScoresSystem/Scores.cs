using System;
using Game.Core.Saves;
using SaveSystem;

namespace Game.ScoresSystem
{
    public class Scores : ILoadSavable
    {
        private const string SAVE_SCORES_KEY = "scores";
        
        private int _scores;
        private ISaveLoader _saveLoader;

        public event Action OnScoresChanged;

        ~Scores() => OnScoresChanged = null;
        
        public void Construct(ISaveLoader saveLoader)
        {
            _saveLoader = saveLoader;
        }

        public int GetScores() => _scores;

        public void IncreaseScores(int value = 1)
        {
            _scores += value;
            OnScoresChanged?.Invoke();
            Save(); 
        }

        public void Load()
        {
            if (_saveLoader.TryLoad<int>(out var result, SAVE_SCORES_KEY))
                _scores = result;
        }

        public void Save()
        {
            _saveLoader.Save(_scores, SAVE_SCORES_KEY); // TODO: maybe add auto-key resolver. Auto place key or file path or null? depends on saver type. Add overload for save class with auto-resolver type instead of key
        }
    }
}