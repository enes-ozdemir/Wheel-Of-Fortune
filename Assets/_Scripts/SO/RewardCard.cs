using UnityEngine;

namespace _Scripts.SO
{
    [CreateAssetMenu]
    public class RewardCard : ScriptableObject
    {
        [Header("Card Sprites")]
        public Sprite commonCard;
        public Sprite bombCard;
        [Header("Frame Sprites")]
        public Sprite bronzeFrame;
        public Sprite silverFrame;
        public Sprite goldFrame;
    }
}