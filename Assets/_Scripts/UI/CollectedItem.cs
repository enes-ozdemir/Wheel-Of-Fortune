using System;
using _Scripts.Manager;
using _Scripts.SO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class CollectedItem : MonoBehaviour
    {
        [SerializeField] private Image itemImage;
        [SerializeField] private TextMeshProUGUI itemAmountText;
        [SerializeField] private TextMeshProUGUI itemNameText;

        private Reward _totalReward;

        public void InitCollectedItem(Reward reward)
        {
            gameObject.SetActive(true);
            itemImage.sprite = AtlasManager.onGetSpriteFromAtlas.Invoke(reward.spriteName);
            itemAmountText.text = reward.amount.ToString();
            itemNameText.text = reward.itemName;
        }

        public void AddItemAmount(int amount)
        {
            _totalReward.amount += amount;
            itemAmountText.text = _totalReward.amount.ToString();
        }
    }
}