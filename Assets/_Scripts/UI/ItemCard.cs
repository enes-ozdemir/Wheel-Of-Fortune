using System;
using _Scripts.Enums;
using _Scripts.Manager;
using _Scripts.SO;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class ItemCard : MonoBehaviour
    {
        [SerializeField] private Image itemImage;
        [SerializeField] private TextMeshProUGUI itemAmount;
        [SerializeField] private TextMeshProUGUI itemName;
        [SerializeField] private Button claimButton;
        [SerializeField] private Button retryButton;
        [SerializeField] private Button giveUp;
        [SerializeField] private Button adButton;
        
        private Reward _reward;

        private void Awake()
        {
            claimButton.onClick.AddListener(CardClicked);
        }

        public void InitCard(Reward reward)
        {
            _reward = reward;
            gameObject.SetActive(true);
            transform.DOScale(0, 0f);
            transform.DOScale(1, 1f).SetEase(Ease.OutBounce);
            itemImage.sprite = AtlasManager.onGetSpriteFromAtlas.Invoke(reward.spriteName);
            itemAmount.text = reward.amount.ToString();
            itemName.text = reward.itemName;
            
            if (reward.rarity == Rarity.Bomb) SetGameOverButtons();
            else SetClaimButton();
        }

        private void CardClicked()
        {
            CollectedItemPanel.onItemCollected.Invoke(_reward);
            transform.DOScale(0, 1f).OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
            WheelController.onWheelStateChanged.Invoke(WheelState.Ready);
        }

        private void SetGameOverButtons()
        {
            claimButton.gameObject.SetActive(false);
            retryButton.gameObject.SetActive(true);
            adButton.gameObject.SetActive(true);
            giveUp.gameObject.SetActive(true);
        }

        private void SetClaimButton()
        {
            claimButton.gameObject.SetActive(true);
            retryButton.gameObject.SetActive(false);
            adButton.gameObject.SetActive(false);
            giveUp.gameObject.SetActive(false);
        }
    }
}