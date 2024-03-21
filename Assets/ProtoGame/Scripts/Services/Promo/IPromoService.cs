
using ProtoGame.Data;
using System.Collections.Generic;

namespace ProtoGame.Services
{
    public interface IPromoService
    {
        IReadOnlyList<IPromoModel> GetPromos();
    }
}
