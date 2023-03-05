using _Scripts.Enums;
using _Scripts.SO;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Manager
{
    public class WheelSkinManager : MonoBehaviour
    {
        [SerializeField] private Image wheelImage;
        [SerializeField] private Image indicatorImage;
        [SerializeField] private Wheel wheel;

        private void OnEnable() => GameManager.onZoneReached += SetSkin;

        private void OnDisable() => GameManager.onZoneReached -= SetSkin;

        private void SetSkin(Zone zoneType)
        {
            switch (zoneType)
            {
                case Zone.NormalZone:
                    Debug.Log("NormalZone reached");
                    SetBronzeSkin();
                    break;
                case Zone.SafeZone:
                    Debug.Log("SafeZone reached");
                    SetSilverSkin();
                    break;
                case Zone.SuperZone:
                    Debug.Log("SuperZone reached");
                    SetGoldSkin();
                    break;
            }
        }

        private void SetGoldSkin()
        {
            wheelImage.sprite = wheel.goldWheelImage;
            indicatorImage.sprite = wheel.goldIndicatorImage;
        }

        private void SetBronzeSkin()
        {
            wheelImage.sprite = wheel.bronzeWheelImage;
            indicatorImage.sprite = wheel.bronzeIndicatorImage;
        }

        private void SetSilverSkin()
        {
            wheelImage.sprite = wheel.silverWheelImage;
            indicatorImage.sprite = wheel.silverIndicatorImage;
        }
    }
}