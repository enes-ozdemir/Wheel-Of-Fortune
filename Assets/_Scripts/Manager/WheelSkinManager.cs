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
            GameManager.onLevelCompleted += SetBronzeSkin;
            GameManager.onSafeZoneReached += SetSilverSkin;
            GameManager.onSuperZoneReached += SetGoldSkin;
        }

        private void OnDisable()
        {
            GameManager.onLevelCompleted -= SetBronzeSkin;
            GameManager.onSafeZoneReached -= SetSilverSkin;
            GameManager.onSuperZoneReached -= SetGoldSkin;
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