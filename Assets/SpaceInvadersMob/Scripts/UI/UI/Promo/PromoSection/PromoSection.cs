using System.Collections.Generic;
using System.Linq;
using SpaceInvadersMob.Data;
using SpaceInvadersMob.Extensions.UI;
using SpaceInvadersMob.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SpaceInvadersMob.UI.Promo
{
    public class PromoSection : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private ScrollRectExt _scrollRect;
        [SerializeField] private GameObject _promoSlotPrefab;

        List<PromoSlot> _promoSlotViews;

        [Inject]
        private IRarityService _rarityService;
        [Inject]

        private IUserService _userService;
        private IGemsViewProvider IGemsViewProvider { get; set;}

        private void ClickSlot(IPromoModel model)
        {
            if (_userService.HasCurrency(model.Cost))
            {
                _userService.ReduceCurrency(model.Cost);
                IGemsViewProvider.ReduceCurrency(model.Cost);
                Debug.Log(model.Title);
                return;
            }

            Debug.LogError("Not enough Gems");

        }

        public void SetParentScroll(ScrollRect parentScroll)
        {
            _scrollRect.ParentScroll = parentScroll;
        }

        public void SetData(PromoType key, List<IPromoModel> models)
        {
            _title.text = key.ToString();
            _promoSlotViews = new List<PromoSlot>(models.Count);

            List<IRarityModel> rarityModels = _rarityService.GetRarities().ToList();
            foreach (IPromoModel model in models)
            {
                PromoSlot promoView = Instantiate(_promoSlotPrefab, _scrollRect.content)
                                                .GetComponent<PromoSlot>();

                promoView.SetData(model, rarityModels.Find(D => D.Rarity == model.Rarity));
                _promoSlotViews.Add(promoView);

                promoView.ClickNotify += ClickSlot;
            }
        }

        public void SetGemsProvider(IGemsViewProvider gemsViewProvider)
        {
            IGemsViewProvider = gemsViewProvider; 
        }


    }
}
