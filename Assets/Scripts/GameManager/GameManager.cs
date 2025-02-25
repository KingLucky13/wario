using LearnGame.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

namespace LearnGame
{
    public class GameManager : MonoBehaviour
    {

        public event Action Win;
        public event Action Loss;

        public PlayerCharacter Player {  get; private set; }
        public List<EnemyCharacter> Enemies { get; private set; }

        public TimerUI Timer { get; private set; }

        [SerializeField]
        private TextMeshProUGUI _remainingEnemiesText;
        private string _format;

        private void Start()
        {
            Player=FindObjectOfType<PlayerCharacter>();
            Enemies = FindObjectsOfType<EnemyCharacter>().ToList();
            Timer = FindObjectOfType<TimerUI>();

            Player.Dead += OnPlayerDead;
            foreach(var enemy in Enemies)
            {
                enemy.Dead += OnEnemyDead;
            }

            Timer.TimeEnd += PlayerLoose;

            _format = _remainingEnemiesText.text;
            _remainingEnemiesText.text = string.Format(_format, Enemies.Count);
        }

        private void OnEnemyDead(BaseCharacter sender)
        {
            var enemy=sender as EnemyCharacter;
            Enemies.Remove(enemy);
            enemy.Dead -= OnEnemyDead;
            _remainingEnemiesText.text = string.Format(_format, Enemies.Count);

            if (Enemies.Count == 0)
            {
                Win?.Invoke();
                Time.timeScale = 0f;
            }
        }

        private void OnPlayerDead(BaseCharacter sender)
        {
            Player.Dead -= OnPlayerDead;
            Loss?.Invoke();
            Time.timeScale = 0f;
        }

        private void PlayerLoose()
        {
            Timer.TimeEnd -= PlayerLoose;
            Loss?.Invoke();
            Time.timeScale = 0f;
        }
    }
}