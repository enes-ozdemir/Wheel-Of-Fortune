using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Scripts
{
    public class RewardManager : MonoBehaviour
    {
        public List<Reward> rewardList;
        public Reward bomb;
        private List<Reward> _zoneRewards = new();
        private const int MaxRewardCount = 8;

        public List<Reward> GetRewards(int currentLevel)
        {
            var currentZone = GetCurrentZone(currentLevel);

            _zoneRewards.Clear();
            if (currentZone == Zone.NormalZone) _zoneRewards.Add(bomb);


            for (int i = _zoneRewards.Count; i < MaxRewardCount; i++)
            {
                var rarity = GetItemRarityForZone(currentZone);
                var reward = AddRandomRewardWithRarity(rarity);
                Debug.Log($"Item generated  {reward.itemName} : {reward.amount}");
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
            if (currentZone == Zone.SafeZone) rarityRoll += 70;

            var rarity = rarityRoll switch
            {
                < 40 => Rarity.Common,
                < 70 => Rarity.Rare,
                < 85 => Rarity.Epic,
                _ => Rarity.Legendary
            };
            Debug.Log($"Item rarity will be {rarity}");
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