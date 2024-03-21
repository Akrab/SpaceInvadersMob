using ProtoGame.Data;
using ProtoGame.UI.Promo;
using TMPro;
using UnityEngine;

namespace RedPanda.Project.UI.Promo
{
    public class PromoSlotCost : MonoBehaviour, IPromoSlotViewPart
    {
        [SerializeField] private TextMeshProUGUI _value;
        [SerializeField] private string _mask;
        public void SetData(IPromoModel promoModel)
        {
            _value.text = string.Format(_mask, promoModel.Cost);
        }
    }
}
