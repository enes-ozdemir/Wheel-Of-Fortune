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
        public static Action<Zone> onZoneReached;
        public static Action onGameRestart;
        public static Action onGameResumed;

        private void OnEnable()
        {
            onLevelCompleted += IncreaseLevel;
            onGameRestart += RestartGame;
            onGameResumed += ResumeGame;
        }

        private void OnDisable()
        {
            onLevelCompleted -= IncreaseLevel;
            onGameRestart -= RestartGame;
            onGameResumed -= ResumeGame;
        }

        private void ResumeGame() => wheelController.SetCurrentLevel(_gameLevel);

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
            onZoneReached.Invoke(Zone.NormalZone);
        }

        private void CheckZone()
        {
            if (_gameLevel % 30 == 0)
            {
                onZoneReached.Invoke(Zone.SuperZone);
            }
            else if (_gameLevel % 5 == 0)
            {
                onZoneReached.Invoke(Zone.SafeZone);
            }
            else
            {
                onZoneReached.Invoke(Zone.NormalZone);
            }
        }
    }
}