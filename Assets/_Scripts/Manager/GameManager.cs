using System;
using _Scripts.UI;
using UnityEngine;

namespace _Scripts.Manager
{
    public class GameManager : MonoBehaviour
    {
        private int _gameLevel = 1;
        [SerializeField] private int zoneCount = 30;
        public Action onLevelCompleted;
        [SerializeField] private ZoneController zoneController;

        private void OnEnable()
        {
            onLevelCompleted += IncreaseLevel;
        }
        
        private void OnDisable()
        {
            onLevelCompleted -= IncreaseLevel;
        }

        private void Awake()
        {
            _gameLevel = 1;
            zoneController.SetZones(zoneCount);
        }

        private void IncreaseLevel()
        {
            _gameLevel++;
        }

    }
}