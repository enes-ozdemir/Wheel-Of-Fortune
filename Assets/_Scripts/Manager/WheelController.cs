using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Enums;
using _Scripts.Manager;
using _Scripts.SO;
using _Scripts.UI;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts
{
    public class WheelController : MonoBehaviour
    {
        [SerializeField] private float rotationDuration;
        [SerializeField] private SpinController spinController;
        [SerializeField] private RewardManager rewardManager;
        [SerializeField] private List<Slot> slotList;
        [SerializeField] private ItemCard itemCard;
        [SerializeField] private Transform wheelTransform;

        public static Action<WheelState> onWheelStateChanged;

        private WheelState _wheelState = WheelState.Ready;
        private List<Reward> _rewards;

        private void OnEnable()
        {
            spinController.onSpinButtonClicked += SpinWheel;
            onWheelStateChanged += SetWheelState;
        }

        private void OnDisable()
        {
            spinController.onSpinButtonClicked -= SpinWheel;
            onWheelStateChanged -= SetWheelState;
        }

        private void Start()
        {
            SetWheelRewards(1);
        }

        private void SetWheelRewards(int level)
        {
            _rewards = rewardManager.GetRewards(level);

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
            float angle = -(45f * randomItemIndex);
            int extraSpins = Random.Range(1, 4);
            int spinCount = 2 + extraSpins;
            float totalAngle = 360f * spinCount - angle;
            var targetAngleVector = Vector3.forward * totalAngle;

            wheelTransform.transform.DORotate(targetAngleVector, rotationDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.OutCirc).OnComplete(() =>
                {
                    print("Rotate completed");
                    itemCard.InitCard(_rewards[randomItemIndex]);
                });

            yield return new WaitForSeconds(rotationDuration);
        }

        private int GetRandomWheelItem()
        {
            var index = Random.Range(0, slotList.Count);
            Debug.Log($"{_rewards[index].name} selected");
            return index;
        }
    }
}