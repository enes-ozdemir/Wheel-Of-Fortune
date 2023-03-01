using _Scripts.Manager;
using _Scripts.SO;
using DG.Tweening;
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
            transform.DOScale(0, 0f);
            transform.DOScale(1, 1f).SetEase(Ease.Linear);
            itemImage.sprite = AtlasManager.onGetSpriteFromAtlas.Invoke(reward.spriteName);
            itemAmountText.text = reward.amount.ToString();
            itemNameText.text = reward.itemName;
            _totalReward = reward;
        }

        public void AddItemAmount(int amount)
        {
            _totalReward.amount += amount;
            itemAmountText.text = _totalReward.amount.ToString();
        }
    }
}