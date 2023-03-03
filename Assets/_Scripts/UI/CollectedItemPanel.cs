using System;
using System.Collections.Generic;
using _Scripts.Enums;
using _Scripts.Manager;
using _Scripts.SO;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class CollectedItemPanel : MonoBehaviour
    {
        private Dictionary<string, CollectedItem> _collectedItemDict = new();
        private List<CollectedItem> _collectedItems = new();
        [SerializeField] private GameObject collectedItemPrefab;
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

        private void CollectRewards()
        {
            print("Rewards collected");
            if (_collectedItems.Count > 0)
            {
                foreach (var item in _collectedItems)
                {
                    print(item.name);
                }
            }

            rewardPanel.SetRewardPanel(_collectedItems);
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
            var collectedItem = newItem.GetComponent<CollectedItem>();
            _collectedItems.Add(collectedItem);
            collectedItem.InitCollectedItem(reward);
            _collectedItemDict.Add(reward.itemName, collectedItem);
        }

        private void ClearPanel()
        {
            foreach (var collectedItem in _collectedItems)
            {
                Destroy(collectedItem.gameObject);
            }

            _collectedItemDict.Clear();
            _collectedItems.Clear();
        }

        private void ShowExitButton(Zone zoneType)
        {
            if (zoneType == Zone.NormalZone) return;
            exitButton.gameObject.SetActive(true);
        }

        private void HideExitButton() => exitButton.gameObject.SetActive(false);
    }
}