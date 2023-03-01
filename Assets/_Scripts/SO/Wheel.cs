using UnityEngine;

namespace _Scripts.SO
{
    [CreateAssetMenu]
    public class Wheel : ScriptableObject
    {
        [Header("Wheel Sprites")]
        public Sprite bronzeWheelImage;
        public Sprite silverWheelImage;
        public Sprite goldWheelImage;
        [Header("Indicator Sprites")]
        public Sprite bronzeIndicatorImage;
        public Sprite silverIndicatorImage;
        public Sprite goldIndicatorImage;
    }
}