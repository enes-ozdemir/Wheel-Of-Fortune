using _Scripts.Manager;
using _Scripts.SO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts
{
    public class Slot : MonoBehaviour
    {
        [SerializeField] private Image itemImage;
        [SerializeField] private TextMeshProUGUI itemAmount;
        [SerializeField] private Image backgroundImage;
        private SlotData _slotData;

        public void InitSlot(Reward reward)
        {
            gameObject.SetActive(true);
            itemImage.sprite = AtlasManager.onGetSpriteFromAtlas.Invoke(reward.itemName);
            itemAmount.text = reward.spriteName;

            var bgImage = reward.rarity switch
            {
                Rarity.Rare => _slotData.rareBg,
                Rarity.Epic => _slotData.epicBg,
                Rarity.Legendary => _slotData.legendaryBg,
                _ => _slotData.commonBg
            };

            backgroundImage.sprite = bgImage.sprite;
        }
    }
}