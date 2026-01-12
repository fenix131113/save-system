using TMPro;
using UnityEngine;

namespace Game.ScoresSystem.View
{
    public class ScoresLabel : MonoBehaviour
    {
        [SerializeField] private TMP_Text label;
        
        private Scores _scores;
        
        public void Construct(Scores scores) => _scores = scores;

        private void Start()
        {
            Bind();
            Draw();
        }

        private void OnDestroy()
        {
            Expose();
        }

        private void Draw() => label.text = _scores.GetScores().ToString();

        private void Bind() => _scores.OnScoresChanged += Draw;

        private void Expose() => _scores.OnScoresChanged -= Draw;
    }
}