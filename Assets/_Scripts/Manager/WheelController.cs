using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Enums;
using _Scripts.SO;
using _Scripts.UI;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace _Scripts.Manager
{
    public class WheelController : MonoBehaviour
    {
        [SerializeField] private float rotationDuration;
        [SerializeField] private RewardManager rewardManager;
        [SerializeField] private ItemCard itemCard;
        [Space]
        [SerializeField] private Button spinButton;
        [SerializeField] private Transform wheelTransform;
        [SerializeField] private List<Slot> slotList;

        private WheelState _wheelState = WheelState.Ready;
        private List<Reward> _rewards;
        private int _currentLevel;

        public static Action<WheelState> onWheelStateChanged;

        private void OnEnable()
        {
            spinButton.onClick.AddListener(SpinWheel);
            onWheelStateChanged += SetWheelState;
        }

        private void OnDisable()
        {
            spinButton.onClick.RemoveListener(SpinWheel);
            onWheelStateChanged -= SetWheelState;
        }

        public void SetCurrentLevel(int level)
        {
            _currentLevel = level;
            SetWheelRewards();
            SetWheelState(WheelState.Ready);
        }

        private void SetWheelRewards()
        {
            _rewards = rewardManager.GetRewards(_currentLevel);

            for (int i = 0; i < slotList.Count; i++)
            {
                slotList[i].InitSlot(_rewards[i]);
            }
        }

        private void SpinWheel()
        {
            if (_wheelState == WheelState.Busy) return;
            StartCoroutine(SpinWheelCo());
        }

        private void SetWheelState(WheelState newState) => _wheelState = newState;

        private IEnumerator SpinWheelCo()
        {
            SetWheelState(WheelState.Busy);
            var randomItemIndex = GetRandomWheelItem();
            var targetAngleVector = SetWheelAngle(randomItemIndex);

            wheelTransform.transform.DORotate(targetAngleVector, rotationDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.OutCirc).OnComplete(() =>
                {
                    Debug.Log("Rotate completed");
                    itemCard.InitCard(_rewards[randomItemIndex]);
                });

            yield return new WaitForSeconds(rotationDuration);
        }

        private Vector3 SetWheelAngle(int randomItemIndex)
        {
            var angle = -(45f * randomItemIndex);
            var extraSpins = Random.Range(3, 6);
            var totalAngle = 360f * extraSpins - angle;
            var targetAngleVector = Vector3.forward * totalAngle;
            return targetAngleVector;
        }

        private int GetRandomWheelItem()
        {
            var index = Random.Range(0, slotList.Count);
            Debug.Log($"{_rewards[index].name} selected");
            return index;
        }
    }
}