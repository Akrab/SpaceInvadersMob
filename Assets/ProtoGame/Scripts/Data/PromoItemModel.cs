using UnityEngine;

namespace ProtoGame.Data
{
    public interface IPromoModel
    {
        public string Title { get; }
        public PromoRarity Rarity { get; }
        public PromoType PromoType { get; }
        public Sprite Icon { get; }
        public int Cost { get; }
    }

    public class PromoItemModel<T> : IPromoModel where T : PromoItemData
    {
        private readonly T _data;
        public PromoRarity Rarity => _data.Rarity;

        public PromoType PromoType => _data.PromoType;

        public Sprite Icon => _data.Icon;

        public int Cost => _data.Cost;

        public string Title => _data.Name;

        public PromoItemModel(T data)
        {
            _data = data;
        }
    }
}
