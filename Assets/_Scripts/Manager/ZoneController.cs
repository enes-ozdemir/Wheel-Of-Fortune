using System;
using System.Collections.Generic;
using _Scripts.UI;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.Manager
{
    public class ZoneController : MonoBehaviour
    {
        [SerializeField] private Transform zoneParent;
        [SerializeField] private ZonePool zonePool;

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
            for (int i = 1; i < zoneCount + 1; i++)
            {
                AddZone(i);
            }
        }

        public void ClearZones()
        {
            foreach (var zoneLevelItem in _zoneLevelList)
            {
                zonePool.pool.Release(zoneLevelItem.gameObject);
            }

            _zoneLevelList.Clear();
        }

        public void AddZone(int level)
        {
            var zone = zonePool.pool.Get();
            zone.transform.SetParent(zoneParent);
            zone.transform.localScale = new Vector3(1, 1, 1);
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