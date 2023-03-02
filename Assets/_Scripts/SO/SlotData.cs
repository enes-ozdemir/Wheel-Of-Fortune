using UnityEngine;

namespace _Scripts.SO
{
    [CreateAssetMenu]
    public class SlotData : ScriptableObject
    {
        [Header("Slot Rarity Colors")]
        public Color commonBg;
        public Color rareBg;
        public Color epicBg;
        public Color legendaryBg;
        public Color bombBg;
    }
}