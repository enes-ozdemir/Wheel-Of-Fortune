using _Scripts.Manager;
using _Scripts.SO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class Slot : MonoBehaviour
    {
        [SerializeField] private Image itemImage;
        [SerializeField] private TextMeshProUGUI itemAmount;
        [SerializeField] private Image backgroundImage;
         [SerializeField] private SlotData slotData;

        public void InitSlot(Reward reward)
        {
            gameObject.SetActive(true);
            itemImage.sprite = AtlasManager.onGetSpriteFromAtlas.Invoke(reward.spriteName);
            itemAmount.text = reward.itemName;

            var bgImage = reward.rarity switch
            {
                Rarity.Rare => slotData.rareBg,
                Rarity.Epic => slotData.epicBg,
                Rarity.Legendary => slotData.legendaryBg,
                _ => slotData.commonBg
            };

            backgroundImage.color = bgImage;
        }
    }
}