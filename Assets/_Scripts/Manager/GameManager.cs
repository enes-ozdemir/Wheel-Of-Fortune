using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private ZoneController zoneController;
      [SerializeField] private WheelController wheelController;

        private int _gameLevel = 1;
        private const int ZoneStartCount = 7;

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
            zoneController.ClearZones();
            zoneController.SetZones(ZoneStartCount);
            zoneController.SetNewBorder();
            wheelController.SetCurrentLevel(_gameLevel);
        }

        private void RestartGame()
        {
            _gameLevel = 1;
            InitGame();
        }

        private void IncreaseLevel()
        {
            print("Level increased");
            zoneController.RemoveZone();
            zoneController.AddZone(ZoneStartCount + _gameLevel);
            _gameLevel++;
            zoneController.SetNewBorder();
            CheckZone();
            wheelController.SetCurrentLevel(_gameLevel);
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