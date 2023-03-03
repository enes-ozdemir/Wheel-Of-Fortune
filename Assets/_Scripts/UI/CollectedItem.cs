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

        private int _totalReward;

        public void InitCollectedItem(Reward reward)
        {
            gameObject.SetActive(true);
            
            StartScaleAnim();

            itemImage.sprite = AtlasManager.onGetSpriteFromAtlas.Invoke(reward.spriteName);
            itemAmountText.text = reward.amount.ToString();
            itemNameText.text = reward.itemName;
            _totalReward = reward.amount;
        }

        public void InitCollectedItem(CollectedItem collectedItem)
        {
            itemImage.sprite = collectedItem.itemImage.sprite;
            itemAmountText.text = collectedItem.itemAmountText.text;
            itemNameText.text = collectedItem.itemNameText.text;
        }

        private void StartScaleAnim()
        {
            transform.DOScale(0, 0f);
            transform.DOScale(1, 0.5f).SetEase(Ease.Linear);
        }

        public void AddItemAmount(int amount)
        {
            _totalReward += amount;
            itemAmountText.text = _totalReward.ToString();
        }
    }
}