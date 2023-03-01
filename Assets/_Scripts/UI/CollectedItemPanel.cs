using System;
using System.Collections.Generic;
using _Scripts.SO;
using UnityEngine;

namespace _Scripts.UI
{
    public class CollectedItemPanel : MonoBehaviour
    {
        private Dictionary<string, CollectedItem> _collectedItemDict = new();
        [SerializeField] private GameObject collectedItemPrefab;
        [SerializeField] private List<GameObject> collectedItems = new();

        public static Action<Reward> onItemCollected;

        private void Awake()
        {
            onItemCollected += CollectItem;
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