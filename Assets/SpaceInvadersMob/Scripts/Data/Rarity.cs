using System;
using UnityEngine;

namespace SpaceInvadersMob.Data
{
    [CreateAssetMenu(fileName = "RarityModels", menuName = "ScriptableObjects/Rarity Models", order = 1)]
    public class Rarity : ScriptableObject
    {

        [SerializeField]
        private RarityData[] _rarityDatas;

        public RarityData[] RarityDatas=> _rarityDatas;

    }

    public enum PromoRarity
    {
        Common,
        Rare,
        Epic
    }

    [Serializable]
    public class RarityData
    {
        [SerializeField]
        private string _name;

        [SerializeField]
        private PromoRarity _rarity;

        [SerializeField]
        private Color _color;

        [SerializeField]
        public Sprite _promoBg;

        public PromoRarity Rarity => _rarity;
        public Color Color => _color;

        public Sprite PromoBg => _promoBg;
    }
}
