using System;
using UnityEngine;

namespace _Scripts.Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private ZoneController zoneController;
        [SerializeField] private WheelController _wheelController;

        private int _gameLevel = 1;
        private const int ZoneStartCount = 5;

        public static Action onLevelCompleted;
        public static Action onSafeZoneReached;
        public static Action onSuperZoneReached;
        public static Action onGameRestart;

        private void OnEnable()
        {
            onLevelCompleted += IncreaseLevel;
            onGameRestart += RestartGame;
        }

        private void OnDisable()
        {
            onLevelCompleted -= IncreaseLevel;
            onGameRestart -= RestartGame;
        }

        private void Start()
        {
            InitGame();
        }

        private void InitGame()
        {
            zoneController.SetZones(ZoneStartCount);
            zoneController.SetNewBorder(_gameLevel);
            _wheelController.SetCurrentLevel(_gameLevel);
        }

        private void RestartGame() => InitGame();

        private void IncreaseLevel()
        {
            print("Level increased");
            _gameLevel++;
            zoneController.AddZone(ZoneStartCount + _gameLevel);
            zoneController.SetNewBorder(_gameLevel);
            CheckZone();
            _wheelController.SetCurrentLevel(_gameLevel);
        }

        private void CheckZone()
        {
            if (_gameLevel % 30 == 0)
            {
                onSuperZoneReached.Invoke();
            }
            else if (_gameLevel % 5 == 0)
            {
                onSafeZoneReached.Invoke();
            }
        }
    }
}