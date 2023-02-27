using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts
{
    public class WheelController : MonoBehaviour
    {
        [SerializeField] private int level;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float rotationDuration;
        private WheelState _wheelState;
        [SerializeField] private SpinController spinController;
        [SerializeField] private RewardManager rewardManager;
        [SerializeField] private List<Slot> slotList;


        private void OnEnable()
        {
            spinController.onSpinButtonClicked += SpinWheel;
        }

        private void OnDisable()
        {
            spinController.onSpinButtonClicked -= SpinWheel;
        }

        private void Start()
        {
            SetWheelRewards(1);
        }

        private void SetWheelRewards(int level)
        {
            var rewards = rewardManager.GetRewards(level);

            for (int i = 0; i < slotList.Count; i++)
            {
                slotList[i].InitSlot(rewards[i]);
            }
        }

        private void SpinWheel()
        {
            Debug.Log("Spin clicked");
            transform.DORotate(Vector3.zero, rotationDuration);
        }
    }
}


public enum WheelState
{
    Ready,
    Busy
}