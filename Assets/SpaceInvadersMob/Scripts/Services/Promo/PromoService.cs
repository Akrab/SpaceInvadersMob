using System.Collections.Generic;
using SpaceInvadersMob.Data;
using SpaceInvadersMob.Infrastructure.Containers;

namespace SpaceInvadersMob.Services
{
    public class PromoService : IPromoService
    {
        private List<IPromoModel> _models = new List<IPromoModel>();
        public PromoService(ConfigContainer configContainer) {
            foreach (var item in configContainer.Get<PromoItems>().Items)
                _models.Add(new PromoItemModel<PromoItemData>(item));
        }
        public IReadOnlyList<IPromoModel> GetPromos()
        {
            return _models;
        }
    }
}
