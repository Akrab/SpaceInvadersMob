using ProtoGame.Data;
using ProtoGame.Services;
using ProtoGame.UI.Promo;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ProtoGame.UI
{
    public class PromoForm : BaseForm<PromoForm>
    {
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private GemsView _gemsView;
        [SerializeField] private GameObject _sectionPrefab;

        [Inject] private IPromoService _promoService;
        [Inject] private IRarityService _rarityService;
        [Inject] private IUserService _userService;

        private void Start()
        {

            var promoModels = _promoService.GetPromos()
                .GroupBy(D => D.PromoType)
                .ToDictionary(k => k.Key, V => V.OrderByDescending(D => D.Rarity).ToList()); ;

            _gemsView.SetData(_userService.Currency);
            foreach (PromoType key in promoModels.Keys)
            {
                var models = promoModels[key];
                PromoSection promoSectionView = Instantiate(_sectionPrefab, _scrollRect.content).GetComponent<PromoSection>();
                _diContainer.Inject(promoSectionView);
                promoSectionView.SetGemsProvider(_gemsView);
                promoSectionView.SetData(key, models);
                promoSectionView.SetParentScroll(_scrollRect);
            }
        }
    }
}
