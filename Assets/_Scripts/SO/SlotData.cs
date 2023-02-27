using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.SO
{
    [CreateAssetMenu]
    public class SlotData : ScriptableObject
    {
        public Image commonBg;
        public Image rareBg;
        public Image epicBg;
        public Image legendaryBg;
    }
}