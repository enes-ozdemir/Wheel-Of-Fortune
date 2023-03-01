using System;
using System.Collections.Generic;
using _Scripts.Manager;
using _Scripts.SO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class CollectedItemPanel : MonoBehaviour
    {
        private Dictionary<string, CollectedItem> _collectedItemDict = new();
        [SerializeField] private GameObject collectedItemPrefab;
        [SerializeField] private List<GameObject> collectedItems = new();
        [SerializeField] private Button exitButton;

        public static Action<Reward> onItemCollected;

        private void OnEnable()
        {
            GameManager.onLevelCompleted += HideExitButton;
            GameManager.onSafeZoneReached += ShowExitButton;
            GameManager.onSuperZoneReached += ShowExitButton;
            GameManager.onGameRestart += ClearPanel;
        }

        private void OnDisable()
        {
            GameManager.onLevelCompleted -= HideExitButton;
            GameManager.onSafeZoneReached -= ShowExitButton;
            GameManager.onGameRestart -= ClearPanel;
        }

        private void Awake()
        {
            onItemCollected += CollectItem;
            exitButton.onClick.AddListener(CollectRewards);
            exitButton.gameObject.SetActive(false);
        }

        private void ShowExitButton() => exitButton.gameObject.SetActive(true);
        private void HideExitButton() => exitButton.gameObject.SetActive(false);

        private void CollectRewards()
        {
            print("Rewards are collected");
            //todo game'i sıfırla
            //bir cıkıs ekranı yap vs.
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
            //todo change this
            var newItem = Instantiate(collectedItemPrefab, transform);
            collectedItems.Add(newItem);
            var collectedItem = newItem.GetComponent<CollectedItem>();
            collectedItem.InitCollectedItem(reward);
            _collectedItemDict.Add(reward.itemName, collectedItem);

        }

        public void ClearPanel()
        {
            foreach (var collectedItem in collectedItems)
            {
                Destroy(collectedItem);
            }

            collectedItems.Clear();
        }
    }
}