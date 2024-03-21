using ProtoGame.Data;
using TMPro;
using UnityEngine;

namespace ProtoGame.UI.Promo
{
    public class PromoSlotName : MonoBehaviour, IPromoSlotViewPart, IPromoRaritySlotViewPart
    {
        [SerializeField] private TextMeshProUGUI _title;

        public void SetData(IPromoModel promoModel)
        {
            _title.text = promoModel.Title;
        }

        public void SetData(IRarityModel rarityModel)
        {
            _title.color = rarityModel.Color;
        }
    }
}
