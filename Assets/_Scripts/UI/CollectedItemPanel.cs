using System;
using System.Collections.Generic;
using _Scripts.Manager;
using _Scripts.SO;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class CollectedItemPanel : MonoBehaviour
    {
        private Dictionary<string, CollectedItem> _collectedItemDict = new();
        [SerializeField] private GameObject collectedItemPrefab;
        private List<CollectedItem> collectedItems = new();
        [SerializeField] private Button exitButton;
        [SerializeField] private RewardPanel rewardPanel;

        public static Action<Reward> onItemCollected;

        private void OnEnable()
        {
            GameManager.onLevelCompleted += HideExitButton;
            GameManager.onZoneReached += ShowExitButton;
            GameManager.onGameRestart += ClearPanel;
        }

        private void OnDisable()
        {
            GameManager.onLevelCompleted -= HideExitButton;
            GameManager.onGameRestart -= ClearPanel;
        }

        private void Awake()
        {
            onItemCollected += CollectItem;
            exitButton.onClick.AddListener(CollectRewards);
            exitButton.gameObject.SetActive(false);
        }

        private void ShowExitButton(Zone zoneType)
        {
            if (zoneType == Zone.NormalZone) return;
            exitButton.gameObject.SetActive(true);
        }

        private void HideExitButton() => exitButton.gameObject.SetActive(false);

        private void CollectRewards()
        {
            print("Rewards are collected");
            if (collectedItems.Count > 0)
            {
                foreach (var item in collectedItems)
                {
                    print(item.name);
                }
            }

            rewardPanel.SetRewardPanel(collectedItems);
            GameManager.onGameRestart.Invoke();
        }

        private void CollectItem(Reward reward)
        {
            if (_collectedItemDict.TryGetValue(reward.itemName, out CollectedItem currentItem))
            {
                currentItem.AddItemAmount(reward.amount);
            }
            else
            {
                NewItemAddedToPanel(reward);
            }

            GameManager.onLevelCompleted.Invoke();
        }

        private void NewItemAddedToPanel(Reward reward)
        {
            var newItem = Instantiate(collectedItemPrefab, transform);
            var item = newItem.GetComponent<CollectedItem>();
            collectedItems.Add(item);
            var collectedItem = newItem.GetComponent<CollectedItem>();
            collectedItem.InitCollectedItem(reward);
            _collectedItemDict.Add(reward.itemName, collectedItem);
        }

        private void ClearPanel()
        {
            foreach (var collectedItem in collectedItems)
            {
                Destroy(collectedItem.gameObject);
            }

            _collectedItemDict.Clear();
            collectedItems.Clear();
        }
    }
}