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
        [SerializeField] private int level;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float rotationDuration;
        private WheelState _wheelState;
        [SerializeField] private SpinController spinController;
        [SerializeField] private RewardManager rewardManager;
        [SerializeField] private List<Slot> slotList;
        [SerializeField] private ItemCard itemCard;
        
        [SerializeField] private GameManager _gameManager;

        private List<Reward> _rewards;

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
            _rewards = rewardManager.GetRewards(level);

            for (int i = 0; i < slotList.Count; i++)
            {
                slotList[i].InitSlot(_rewards[i]);
            }
        }

        private void SpinWheel()
        {
            StartCoroutine(SpinWheelCo());
        }
        
        // private void SpinWheel()
        // {
        //     Debug.Log("Spin clicked");
        //     var startAngle = transform.eulerAngles.z;
        //     var anglePerSection = (360 / 8);
        //     var randomItemIndex = GetRandomWheelItem(8);
        //     var targetAngle = (4 * 360) + anglePerSection * randomItemIndex - startAngle;
        //     var targetAngleVector = Vector3.forward * targetAngle;
        //    // transform.DORotate(targetAngleVector, rotationDuration);
        //
        //    float angle = -(45 * randomItemIndex);
        //    float rightOffset = (angle - 13f) % 360;
        //    float leftOffset = (angle + 13) % 360;
        //    float randomAngle = Random.Range(leftOffset, rightOffset);
        //    int extraSpins = Random.Range(3, 6); // Generates a random number of extra spins between 1 and 3
        //
        //    Vector3 targetRotation = Vector3.forward * (randomAngle + (360 * (2 + extraSpins)));
        //  transform.DORotate(targetRotation, rotationDuration);
        //
        // }
        // private IEnumerator SpinWheelCo()
        // {
        //     var randomItemIndex = GetRandomWheelItem(8);
        //     var angle = (4 * 360) + (360 / 8) * randomItemIndex - transform.eulerAngles.z;
        //    // float angle = -(45 * randomItemIndex);
        //     float rightOffset = (angle - 13f) % 360;
        //     float leftOffset = (angle + 13) % 360;
        //     float randomAngle = Random.Range(leftOffset, rightOffset);
        //     int extraSpins = Random.Range(1, 4);
        //
        //     var targetRotation = Vector3.forward * (randomAngle + (360 * (2 + extraSpins)));
        //
        //     transform.DORotate(targetRotation, rotationDuration, RotateMode.FastBeyond360)
        //         .SetEase(Ease.OutCirc);
        //
        //     yield return new WaitForSeconds(rotationDuration);
        // }

        public IEnumerator SpinWheelCo()
        {
            var randomItemIndex = GetRandomWheelItem(8);
            int sliceCount = 8;
            float sliceAngle = 360f / sliceCount;

            float angle = -(45f * randomItemIndex);
            float rightOffset = (angle - 13f) % 360f;
            float leftOffset = (angle + 13f) % 360f;
            float randomAngle = Random.Range(leftOffset, rightOffset);
            int extraSpins = Random.Range(1, 4);

            int spinCount = 2 + extraSpins;
            float totalAngle = (spinCount * 360f) + (sliceAngle * (sliceCount - randomItemIndex));

            Vector3 targetRotation = Vector3.forward * totalAngle;
            float duration = 3f;

            transform.DORotate(targetRotation, duration, RotateMode.FastBeyond360)
                .SetEase(Ease.OutCirc).OnComplete((() =>
                {
                    itemCard.InitCard(_rewards[randomItemIndex]);
                }));

            yield return new WaitForSeconds(duration);
        }

        public void Spin()
        {
            var index = GetRandomWheelItem(8);
                float angle = -(45 * index);
                float rightOffset = (angle - 13f) % 360;
                float leftOffset = (angle + 13) % 360;
                float randomAngle = Random.Range(leftOffset, rightOffset);
                int extraSpins = Random.Range(1, 4);
              //  Vector3 targetRotation = Vector3.back * (randomAngle + (2 * 360 * rotationDuration));
                float totalAngle = (3 * 360f) + (randomAngle * (8 - index));
                Vector3 targetRotation = Vector3.forward * totalAngle;


                float prevAngle;
                prevAngle = transform.eulerAngles.z;
                float sumDeltaRotationZ = 0;

                transform
                    .DORotate(targetRotation, rotationDuration, RotateMode.Fast)
                    .SetEase(Ease.InOutQuart)
                    .OnUpdate(() =>
                    {

                        float deltaRotationZ = Mathf.Abs(transform.eulerAngles.z - prevAngle);
                        sumDeltaRotationZ = +deltaRotationZ;
                        if (sumDeltaRotationZ >= 45f)
                        {
                            sumDeltaRotationZ = 0;
                           // audioSource.PlayOneShot(audioSource.clip);
                        }
                        prevAngle = transform.eulerAngles.z;

                    })
                    .OnComplete(() =>
                    {
                        Debug.Log("Spin colmpleted");
                        _gameManager.onLevelCompleted.Invoke();
                        //StartCoroutine(SpinCompleted(1));
                    });
        }

        private int GetRandomWheelItem(int itemsHandlersCurrentlySpawned)
        {
            var index = Random.Range(0, slotList.Count);
            Debug.Log($"{_rewards[index].name} selected" );
            return index;

        }
    }
}


public enum WheelState
{
    Ready,
    Busy
}