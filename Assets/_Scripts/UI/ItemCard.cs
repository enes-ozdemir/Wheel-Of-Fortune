using System;
using _Scripts.Enums;
using _Scripts.Manager;
using _Scripts.SO;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class ItemCard : MonoBehaviour
    {
        [SerializeField] private Image itemImage;
        [SerializeField] public Image cardBg;
        [SerializeField] public Image cardFrame;
        [SerializeField] private TextMeshProUGUI itemAmount;
        [SerializeField] private TextMeshProUGUI itemName;

        [SerializeField] private Button claimButton;
        [FormerlySerializedAs("retryButton")] [SerializeField] private Button reviveButton;
        [SerializeField] private Button giveUp;
        [SerializeField] private Button adButton;
        [SerializeField] public RewardCard rewardCard;

        private Reward _reward;

        private void Awake()
        {
            claimButton.onClick.AddListener(CardClicked);
            reviveButton.onClick.AddListener(RestartGame);
            giveUp.onClick.AddListener(RestartGame);
            adButton.onClick.AddListener(ShowAd);
        }

        private void ShowAd()
        {
        }

        private void RestartGame()
        {
            print("Game restart");
            GameManager.onGameRestart.Invoke();
            gameObject.SetActive(false);
        }

        public void InitCard(Reward reward)
        {
            _reward = reward;
            gameObject.SetActive(true);
            SetAnimations();
            itemImage.sprite = AtlasManager.onGetSpriteFromAtlas.Invoke(reward.spriteName);
            var amount = reward.amount;
            if (amount > 1) itemAmount.text = reward.amount.ToString();
            else
            {
                itemAmount.text = "";
            }
            itemName.text = reward.itemName;

            SetCardWithRarity(reward);
        }

        private void SetAnimations()
        {
            transform.DOScale(0, 0f);
            transform.DOScale(1, 1f).SetEase(Ease.OutBounce);
        }

        private void SetCardWithRarity(Reward reward)
        {
            switch (reward.rarity)
            {
                case Rarity.Rare:
                    cardFrame.sprite = rewardCard.silverFrame;
                    break;
                case Rarity.Epic:
                    cardFrame.sprite = rewardCard.silverFrame;
                    break;
                case Rarity.Legendary:
                    cardFrame.sprite = rewardCard.goldFrame;
                    break;
                case Rarity.Bomb:
                    cardBg.sprite = rewardCard.bombCard;
                    SetGameOverButtons();
                    return;
            }

            SetClaimButton();
        }

        private void CardClicked()
        {
            CollectedItemPanel.onItemCollected.Invoke(_reward);
            transform.DOScale(0, 0.4f).OnComplete(() => { gameObject.SetActive(false); });
            WheelController.onWheelStateChanged.Invoke(WheelState.Ready);
        }

        private void SetGameOverButtons()
        {
            claimButton.gameObject.SetActive(false);
            reviveButton.gameObject.SetActive(true);
            adButton.gameObject.SetActive(true);
            giveUp.gameObject.SetActive(true);
        }

        private void SetClaimButton()
        {
            claimButton.gameObject.SetActive(true);
            reviveButton.gameObject.SetActive(false);
            adButton.gameObject.SetActive(false);
            giveUp.gameObject.SetActive(false);
        }
        
        private void OnDestroy()
        {
            claimButton.onClick.RemoveAllListeners();
            reviveButton.onClick.RemoveAllListeners();
            giveUp.onClick.RemoveAllListeners();
            adButton.onClick.RemoveAllListeners();
        }
        
    }
}