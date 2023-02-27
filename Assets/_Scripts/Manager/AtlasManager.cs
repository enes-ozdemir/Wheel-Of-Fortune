using System;
using UnityEngine;
using UnityEngine.U2D;

namespace _Scripts.Manager
{
    public class AtlasManager : MonoBehaviour
    {
        [SerializeField] public SpriteAtlas rewardSpriteAtlas;

        public static Func<string, Sprite> onGetSpriteFromAtlas;

        private void OnEnable() => onGetSpriteFromAtlas += GetSpriteFromAtlas;

        private void OnDisable() => onGetSpriteFromAtlas -= GetSpriteFromAtlas;

        private Sprite GetSpriteFromAtlas(string spriteName) => rewardSpriteAtlas.GetSprite(spriteName);
    }
}