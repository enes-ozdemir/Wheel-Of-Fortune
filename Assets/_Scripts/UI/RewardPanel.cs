﻿using System;
using System.Collections.Generic;
using _Scripts.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class RewardPanel : MonoBehaviour
    {
        private List<CollectedItem> _collectedItemList = new();
        [SerializeField] private Button claimButton;
        [SerializeField] private GameObject _collectedItemPrefab;

        private void OnEnable() => claimButton.onClick.AddListener(Claim);

        private void OnDisable() => claimButton.onClick.RemoveAllListeners();

        private void Claim()
        {
            GameManager.onGameRestart.Invoke();
            transform.parent.gameObject.SetActive(false);
        }

        public void SetRewardPanel(List<CollectedItem> collectedItemList)
        {
            print("Reward panel open");
            transform.parent.gameObject.SetActive(true);
            if (collectedItemList.Count > 0) ClearList();
            foreach (var collectedItem in collectedItemList)
            {
                print("Reward panel inst");
                var prefab = Instantiate(_collectedItemPrefab, transform);
                var collected = prefab.GetComponent<CollectedItem>();
                collected.InitCollectedItem(collectedItem);
                _collectedItemList.Add(collected);
            }
        }

        private void ClearList()
        {
            foreach (var prefab in _collectedItemList)
            {
                Destroy(prefab.gameObject);
            }
        }
    }
}