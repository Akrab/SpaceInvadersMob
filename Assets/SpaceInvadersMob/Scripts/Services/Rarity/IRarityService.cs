using System.Collections.Generic;
using SpaceInvadersMob.Data;

namespace SpaceInvadersMob.Services
{
    public interface IRarityService
    {
        IReadOnlyList<IRarityModel> GetRarities();
    }
}
