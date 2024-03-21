using System;
using UnityEngine;

namespace ProtoGame.Data
{
    [CreateAssetMenu(fileName = "ItemModels", menuName = "ScriptableObjects/Item Models", order = 2)]
    public class PromoItems : ScriptableObject
    {

        [SerializeField]
        private PromoItemData[] _items;

        public PromoItemData[] Items => _items;

    }

    public enum PromoType
    {
        Chest,
        Special,
        InApp
    }

    [Serializable]
    public class PromoItemData
    {
        [SerializeField]
        private string _name;

        [SerializeField]
        private PromoRarity _rarity;

        [SerializeField]
        private PromoType _promoType;

        [SerializeField]
        private Sprite _icon;

        [SerializeField]
        private int _cost;

        public PromoRarity Rarity => _rarity;
        public PromoType PromoType => _promoType;
        public Sprite Icon => _icon;
        public int Cost => _cost;
        public string Name => _name;
    }
}
