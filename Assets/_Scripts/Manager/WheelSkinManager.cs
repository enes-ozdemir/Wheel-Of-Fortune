using System;
using _Scripts.SO;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Manager
{
    public class WheelSkinManager : MonoBehaviour
    {
        [SerializeField] public Image wheelImage;
        [SerializeField] public Image indicatorImage;
        [SerializeField] private Wheel wheel;

        private void OnEnable()
        {
            GameManager.onZoneReached += SetSkin;
        }

        private void OnDisable()
        {
            GameManager.onZoneReached -= SetSkin;
        }

        private void SetSkin(Zone zoneType)
        {
            switch (zoneType)
            {
                case Zone.NormalZone:
                    SetBronzeSkin();
                    break;
                case Zone.SafeZone:
                    SetSilverSkin();
                    break;
                case Zone.SuperZone:
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