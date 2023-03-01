using System.Collections;
using System.Collections.Generic;
using _Scripts.Manager;
using _Scripts.SO;
using _Scripts.UI;
using DG.Tweening;
using UnityEngine;

namespace _Scripts
{
    public class WheelController : MonoBehaviour
    {
        [SerializeField] private float rotationDuration;
        private WheelState _wheelState;
        [SerializeField] private SpinController spinController;
        [SerializeField] private RewardManager rewardManager;
        [SerializeField] private List<Slot> slotList;
        [SerializeField] private ItemCard itemCard;
        [SerializeField] private Transform wheelTransform;

        private List<Reward> _rewards;

        private void OnEnable() => spinController.onSpinButtonClicked += SpinWheel;

        private void OnDisable() => spinController.onSpinButtonClicked -= SpinWheel;

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

        private void SpinWheel() => StartCoroutine(SpinWheelCo());

        private IEnumerator SpinWheelCo()
        {
            var randomItemIndex = GetRandomWheelItem();
            float angle = -(45f * randomItemIndex);
            int extraSpins = Random.Range(1, 4);
            int spinCount = 2 + extraSpins;
            float totalAngle = 360f * spinCount - angle;
            var targetAngleVector = Vector3.forward * totalAngle;

            wheelTransform.transform.DORotate(targetAngleVector, rotationDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.OutCirc).OnComplete((() =>
                {
                    print("Rotate completed");
                    itemCard.InitCard(_rewards[randomItemIndex]);
                }));

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


public enum WheelState
{
    Ready,
    Busy
}