using System;
using Game.Core.Saves;
using SaveSystem;
using UnityEngine;

namespace Game.ScoresSystem
{
    public class Scores : ILoadSavable
    {
        private const string SAVE_SCORES_KEY = "scores";
        
        private int _scoreCount;
        private ISaveLoader _saveLoader;

        public event Action OnScoresChanged;

        ~Scores() => OnScoresChanged = null;
        
        public void Construct(ISaveLoader saveLoader)
        {
            _saveLoader = saveLoader;
        }

        public int GetScores() => _scoreCount;

        public void IncreaseScores(int value = 1)
        {
            _scoreCount += value;
            OnScoresChanged?.Invoke();
            Save(); 
        }

        public void Load()
        {
            if (_saveLoader.TryLoad<ScoreModel>(out var result, SAVE_SCORES_KEY))
                _scoreCount = result.score;
        }

        public void Save()
        {
            _saveLoader.Save(new ScoreModel(_scoreCount), SAVE_SCORES_KEY); // TODO: maybe add auto-key resolver. Auto place key or file path or null? depends on saver type. Add overload for save class with auto-resolver type instead of key
        }

        [Serializable]
        private class ScoreModel
        {
            public int score;

            public ScoreModel(int score) => this.score = score;
        }

        private class ScoresConverter : SaveLoadConverter<ScoreModel>
        {
            protected override string SerializeTyped(ScoreModel obj)
            {
                return JsonUtility.ToJson(obj);
            }

            protected override ScoreModel DeserializeTyped(string data)
            {
                return JsonUtility.FromJson<ScoreModel>(data);
            }
        }
    }
}