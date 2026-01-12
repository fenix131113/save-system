using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Game.ScoresSystem.View
{
    public class ScoresButton : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private int baseScore;
        
        private Scores _scores;
        private bool _isHolding;
        private float _holdingTime;

        public void Construct(Scores scores) => _scores = scores;

        private void Update()
        {
            if (_isHolding)
                _holdingTime += Time.deltaTime;
            
            if(_isHolding && Mouse.current.leftButton.wasReleasedThisFrame)
                OnHoldReleased();
        }

        private void OnHoldReleased()
        {
            _scores.IncreaseScores(baseScore * (int)_holdingTime);
            _isHolding = false;
            _holdingTime = 0;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isHolding = true;
        }
    }
}