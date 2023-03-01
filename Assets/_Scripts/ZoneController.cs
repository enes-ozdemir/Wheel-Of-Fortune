using System;
using System.Collections.Generic;
using _Scripts.Manager;
using _Scripts.UI;
using UnityEngine;

namespace _Scripts
{
    public class ZoneController : MonoBehaviour
    {
        [SerializeField] private GameObject zonePrefab;
        [SerializeField] private Transform zoneParent;

        [Header("Zone Sprites")] [SerializeField]
        private Sprite emptyZone;

        [SerializeField] private Sprite safeZone;
        [SerializeField] private Sprite superZone;

        private List<ZoneLevelItem> _zoneLevelList = new();

        public void SetNewBorder(int gameLevel)
        {
            var index = gameLevel - 1;
            _zoneLevelList[index].AddBorderImage();
            if (index >= 1) _zoneLevelList[index - 1].RemoveBorderImage();
        }

        public void SetZones(int zoneCount)
        {
            Debug.Log($"Zones are set {zoneCount}");
            for (int i = 0; i < zoneCount; i++)
            {
                AddZone(i);
            }
        }

        public void AddZone(int level)
        {
            print($"new zone added {level}");
            var zone = Instantiate(zonePrefab, zoneParent);
            var zoneLevelItem = zone.GetComponent<ZoneLevelItem>();
            var currentZoneSprite = GetZoneSprite(level);
            zoneLevelItem.InitLevel(level, currentZoneSprite);
            _zoneLevelList.Add(zoneLevelItem);
        }

        private Sprite GetZoneSprite(int zoneNumber)
        {
            if (zoneNumber % 5 == 0) return safeZone;
            if (zoneNumber % 30 == 0) return superZone;
            return emptyZone;
        }

    }
}