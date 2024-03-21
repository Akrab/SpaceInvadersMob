using ProtoGame.Data;
using UnityEngine;
using UnityEngine.UI;

namespace ProtoGame.UI.Promo
{
    public class PromoSlotIco : MonoBehaviour, IPromoSlotViewPart
    {
        [SerializeField] private Image _ico;

        public void SetData(IPromoModel promoModel)
        {
            _ico.sprite = promoModel.Icon;
        }
    }
}

