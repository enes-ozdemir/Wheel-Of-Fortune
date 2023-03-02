using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts
{
    public class LevelZoneUI : MonoBehaviour
    {
        [SerializeField] private Scrollbar _scrollbar;
        [SerializeField] private float value;

        private void Update()
        {
            _scrollbar.value = value;
        }
    }
}