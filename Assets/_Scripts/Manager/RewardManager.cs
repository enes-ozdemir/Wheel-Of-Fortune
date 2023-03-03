using System.Collections.Generic;
using System.Linq;
using _Scripts.Enums;
using _Scripts.SO;
using UnityEngine;

namespace _Scripts.Manager
{
    public class RewardManager : MonoBehaviour
    {
        [SerializeField] private List<Reward> rewardList;
        [SerializeField] private Reward bomb;

        private List<Reward> _zoneRewards = new();
        private const int WheelSliceCount = 8;

        public List<Reward> GetRewards(int currentLevel)
        {
            var currentZone = GetCurrentZone(currentLevel);

            _zoneRewards.Clear();
            if (currentZone == Zone.NormalZone) _zoneRewards.Add(bomb);

            for (int i = _zoneRewards.Count; i < WheelSliceCount; i++)
            {
                var rarity = GetItemRarityForZone(currentZone);
                var reward = AddRandomRewardWithRarity(rarity);
                _zoneRewards.Add(reward);
            }

            return _zoneRewards;
        }

        private Reward AddRandomRewardWithRarity(Rarity rarity)
        {
            var matchingRarityRewards = rewardList.Where(x => x.rarity == rarity).ToList();
            if (matchingRarityRewards.Count > 0)
            {
                var reward = matchingRarityRewards[Random.Range(0, matchingRarityRewards.Count)];
                return reward;
            }

            Debug.LogError($"There is no item at Rarity {rarity}");
            return rewardList[0];
        }

        private Rarity GetItemRarityForZone(Zone currentZone)
        {
            int rarityRoll = Random.Range(0, 100);
            if (currentZone == Zone.SafeZone) rarityRoll += 50;
            if (currentZone == Zone.SuperZone) rarityRoll += 70;

            var rarity = rarityRoll switch
            {
                < 40 => Rarity.Common,
                < 70 => Rarity.Rare,
                < 85 => Rarity.Epic,
                _ => Rarity.Legendary
            };
            return rarity;
        }

        private static Zone GetCurrentZone(int currentLevel)
        {
            var currentZone = Zone.NormalZone;
            if (currentLevel % 5 == 0) currentZone = Zone.SafeZone;
            if (currentLevel % 30 == 0) currentZone = Zone.SuperZone;
            return currentZone;
        }
    }
}