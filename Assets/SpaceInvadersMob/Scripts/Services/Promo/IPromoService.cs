using System.Collections.Generic;
using SpaceInvadersMob.Data;

namespace SpaceInvadersMob.Services
{
    public interface IPromoService
    {
        IReadOnlyList<IPromoModel> GetPromos();
    }
}
