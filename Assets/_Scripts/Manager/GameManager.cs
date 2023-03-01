using System;
using UnityEngine;

namespace _Scripts.Manager
{
    public class GameManager : MonoBehaviour
    {
        private int _gameLevel = 1;
        private const int ZoneStartCount = 5;
        public static Action onLevelCompleted;
        public static Action onSpecialZoneReached;
        [SerializeField] private ZoneController zoneController;

        private void OnEnable() => onLevelCompleted += IncreaseLevel;

        private void OnDisable() => onLevelCompleted -= IncreaseLevel;

        private void Awake()
        {
            _gameLevel = 1;
            zoneController.SetZones(ZoneStartCount);
            zoneController.SetNewBorder(_gameLevel);
        }

        private void IncreaseLevel()
        {
            print("Level increased");
            _gameLevel++;
            zoneController.AddZone(ZoneStartCount + _gameLevel);
            zoneController.SetNewBorder(_gameLevel);
            CheckZone();
        }

        private void CheckZone()
        {
            if (_gameLevel % 30 == 0 || _gameLevel % 5 == 0) onSpecialZoneReached.Invoke();
        }
    }
}