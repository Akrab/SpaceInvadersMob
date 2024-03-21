using UnityEngine;

namespace ProtoGame.Data
{
    public interface IRarityModel
    {
        public PromoRarity Rarity { get; }
        public Color Color { get; }

        public Sprite PromoBg { get; }
    }

    public class RarityModel<T> : IRarityModel where T : RarityData
    {

        private readonly T _data;

        public PromoRarity Rarity => _data.Rarity;

        public Color Color => _data.Color;

        public Sprite PromoBg => _data.PromoBg;

        public RarityModel(T data)
        {
            _data = data;
        }
    }
}
