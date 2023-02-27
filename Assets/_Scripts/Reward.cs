using UnityEngine;

namespace _Scripts
{
    [CreateAssetMenu]
    public class Reward : ScriptableObject
    {
        public string itemName;
        public string spriteName;
        public int amount = 1;
        public Rarity rarity;
    }

    public enum Rarity
    {
        Common,Rare,Epic,Legendary
    }
}