using System.Globalization;
using TMPro;
using UnityEngine;

namespace Game.GameTimerSystem.View
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField] private TMP_Text timerText;
        [SerializeField] private GameTimer gameTimer;

        private void Update() => timerText.text = gameTimer.GetTimer().ToString(CultureInfo.InvariantCulture);
    }
}