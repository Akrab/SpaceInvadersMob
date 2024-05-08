using SpaceInvadersMob.Infrastructure.Pools;
using Zenject;

namespace SpaceInvadersMob.Game.Weapons
{
    public class EpicWeapon: BaseWeapon
    {

        [Inject]
        private ProjectileSpherePool _projectileSpherePool;
        
        protected override void CreateProjectile()
        {
            for (int i = 0; i < _spawnPoints.Length; i++)
                _projectileSpherePool.Get().transform.position = _spawnPoints[i].position;
        }
    }
}