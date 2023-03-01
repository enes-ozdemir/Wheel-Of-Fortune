using UnityEngine;

namespace _Scripts.UI
{
    public class ZoneController : MonoBehaviour
    {
        [SerializeField] private GameObject zonePrefab;
        [SerializeField] private Transform zoneParent;

        [Header("Zone Sprites")]
        [SerializeField] private Sprite emptyZone;
        [SerializeField] private Sprite safeZone;
        [SerializeField] private Sprite superZone;

        public void SetZones(int zoneCount)
        {
            for (int i = 0; i < zoneCount; i++)
            {
                var zone = Instantiate(zonePrefab, zoneParent);
                var zoneLevelItem = zone.GetComponent<ZoneLevelItem>();
                var currentZoneSprite = GetZoneSprite(i);
                zoneLevelItem.InitLevel(i, currentZoneSprite);
            }
        }

        private Sprite GetZoneSprite(int zoneNumber)
        {
            if (zoneNumber % 5 == 0) return safeZone;
            if (zoneNumber % 30 == 0) return superZone;
            return emptyZone;
        }
    }
}