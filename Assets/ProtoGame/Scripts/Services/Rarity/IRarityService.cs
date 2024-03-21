
using ProtoGame.Data;
using System.Collections.Generic;

namespace ProtoGame.Services
{
    public interface IRarityService
    {
        IReadOnlyList<IRarityModel> GetRarities();
    }
}
