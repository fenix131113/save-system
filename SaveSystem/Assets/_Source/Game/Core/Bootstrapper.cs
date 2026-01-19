using Game.GameTimerSystem;
using Game.ScoresSystem;
using Game.ScoresSystem.View;
using SaveSystem;
using SaveSystem.SaveLoaders;
using UnityEngine;

namespace Game.Core
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private ScoresButton scoresButton;
        [SerializeField] private ScoresLabel scoresLabel;
        [SerializeField] private GameTimer gameTimer;
        
        private ISaveLoader _saveLoader;
        private Scores _scores;
        private GameSaveLoader _gameSaveLoader;

        private void Awake()
        {
            
            _saveLoader = new PlayerPrefsSaveLoader();
            
            _scores = new Scores();
            _scores.Construct(_saveLoader);
            
            scoresButton.Construct(_scores);
            scoresLabel.Construct(_scores);
            
            gameTimer.Construct(_saveLoader);
            
            _gameSaveLoader = new GameSaveLoader(_scores, gameTimer);
            _gameSaveLoader.Load();
        }
    }
}