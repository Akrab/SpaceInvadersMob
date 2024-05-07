using SpaceInvadersMob.Data;
using SpaceInvadersMob.Extensions.UI;
using UnityEngine;

namespace SpaceInvadersMob.UI.Promo
{
    public class PromoSlot : MonoBehaviour
    {
        public delegate void ClickHandler(IPromoModel model);
        public event ClickHandler ClickNotify;

        [SerializeField] private ButtonExt _btn;

        private IPromoSlotViewPart[] _promoSlotViewParts;
        private IPromoRaritySlotViewPart[] _promoRaritySlotViewParts;
        private IPromoModel _model;

        private void Awake()
        {
            _btn.onClick.AddListener(ClickBuy);
        }
        private void ClickBuy()
        {
            ClickNotify?.Invoke(_model);
        }

        public void SetData(IPromoModel model, IRarityModel rarityModel)
        {
            _promoSlotViewParts = GetComponentsInChildren<IPromoSlotViewPart>();
            _promoRaritySlotViewParts = GetComponentsInChildren<IPromoRaritySlotViewPart>();

            _model = model;

            foreach (IPromoSlotViewPart item in _promoSlotViewParts)
                item.SetData(model);

            foreach (IPromoRaritySlotViewPart item in _promoRaritySlotViewParts)
                item.SetData(rarityModel);
        }
    }
}
