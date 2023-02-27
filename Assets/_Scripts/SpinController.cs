using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts
{
    public class SpinController : MonoBehaviour
    {
        private Button _spinButton;
        public Action onSpinButtonClicked;

        private void OnValidate()
        {
            _spinButton = GetComponent<Button>();
        }

        private void OnEnable()
        {
            if (_spinButton != null)
            {
                _spinButton.onClick.AddListener(SpinButtonClicked);
            }
        }

        private void SpinButtonClicked()
        {
            onSpinButtonClicked.Invoke();
        }

        private void OnDisable()
        {
            _spinButton.onClick.RemoveAllListeners();
        }
    }
}