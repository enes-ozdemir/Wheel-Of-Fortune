using System;
using _Scripts.Enums;
using UnityEngine;

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
            onZoneReached.Invoke(Zone.NormalZone);
            InitGame();
        }

        private void ResumeGame() => wheelController.SetCurrentLevel(_gameLevel);

        private void IncreaseLevel()
        {
            Debug.Log("Level increased");
            zoneController.RemoveFirstZone();
            zoneController.AddZone(ZoneStartCount + _gameLevel);
            _gameLevel++;
            zoneController.SetNewBorder();
            SetReachedZone();
            wheelController.SetCurrentLevel(_gameLevel);
        }

        private void SetReachedZone()
        {
            if (_gameLevel % 30 == 0)
            {
                Debug.Log("SuperZone reached");
                onZoneReached.Invoke(Zone.SuperZone);
            }
            else if (_gameLevel % 5 == 0)
            {
                Debug.Log("SafeZone reached");
                onZoneReached.Invoke(Zone.SafeZone);
            }
            else
            {
                Debug.Log("NormalZone reached");
                onZoneReached.Invoke(Zone.NormalZone);
            }
        }
    }
}