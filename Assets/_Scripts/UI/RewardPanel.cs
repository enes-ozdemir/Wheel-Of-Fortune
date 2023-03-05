using System.Collections.Generic;
using _Scripts.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class RewardPanel : MonoBehaviour
    {
        [SerializeField] private Button claimButton;
        private List<CollectedItem> _collectedItemList = new();

        private void OnEnable() => claimButton.onClick.AddListener(Claim);

        private void OnDisable() => claimButton.onClick.RemoveAllListeners();

        private void Claim()
        {
            GameManager.onGameRestart.Invoke();
            transform.parent.gameObject.SetActive(false);
        }

        public void SetRewardPanel(List<CollectedItem> collectedItemList)
        {
            Debug.Log("Reward panel open");
            transform.parent.gameObject.SetActive(true);
            if (collectedItemList.Count > 0) ClearList();
            foreach (var collectedItem in collectedItemList)
            {
                Instantiate(collectedItem.gameObject, transform);
                collectedItem.InitCollectedItem(collectedItem);
                _collectedItemList.Add(collectedItem);
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