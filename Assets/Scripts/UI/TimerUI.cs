using System.Collections;
using UnityEngine;
using System;
using TMPro;

namespace LearnGame
{
    public class TimerUI : MonoBehaviour
    {
        public event Action TimeEnd;

        [SerializeField]
        private TextMeshProUGUI _outputText;
        private string _format;

        [field:SerializeField]
        public float GameDurationSec {  get; private set; }

        public float TimerSeconds { get; private set; }

        private bool _timerEnd;
        private void Start ()
        {
            _format=_outputText.text;
            _timerEnd = false;
        }

        private void Update ()
        {
            if (_timerEnd) { return; }
            TimerSeconds += Time.deltaTime;
            if (TimerSeconds >= GameDurationSec)
            {
                TimeEnd?.Invoke();
                _timerEnd = true;
            }
            int time=(int)(GameDurationSec-TimerSeconds);
            _outputText.text =string.Format(_format,time/60,time%60);
        }

    }
}