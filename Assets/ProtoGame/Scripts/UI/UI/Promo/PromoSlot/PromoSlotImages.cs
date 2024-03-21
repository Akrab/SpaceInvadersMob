using ProtoGame.Data;
using UnityEngine;
using UnityEngine.UI;

namespace ProtoGame.UI.Promo
{
    public class PromoSlotImages : MonoBehaviour, IPromoRaritySlotViewPart
    {
        [SerializeField] private Image _bg;
        [SerializeField] private Image _line;

        public void SetData(IRarityModel rarityModel)
        {
             _bg.sprite = rarityModel.PromoBg;
            _line.color = rarityModel.Color;
        }
    }
}