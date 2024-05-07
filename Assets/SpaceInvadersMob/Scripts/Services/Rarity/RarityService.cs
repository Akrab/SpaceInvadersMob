using System.Collections.Generic;
using SpaceInvadersMob.Data;
using SpaceInvadersMob.Infrastructure.Containers;

namespace SpaceInvadersMob.Services
{
    public class RarityService : IRarityService
    {
        private List<IRarityModel> _models = new List<IRarityModel>();

        public RarityService(ConfigContainer configContainer) {

            foreach ( var item in configContainer.Get<Rarity>().RarityDatas)
                _models.Add(new RarityModel<RarityData>(item));
        }

        public IReadOnlyList<IRarityModel> GetRarities()
        {
            return _models;
        }
    }
}
