using SpaceInvadersMob.Infrastructure.Pools;
using Zenject;

namespace SpaceInvadersMob.Game.Weapons
{
    public class MegaWeapon: BaseWeapon
    {

        [Inject]
        private ProjectileLinePool _projectileLinePool;
        
        protected override void CreateProjectile()
        {
            _projectileLinePool.Get().transform.position = _spawnPoints[1].position;
            _projectileLinePool.Get().transform.position = _spawnPoints[2].position;
        }
    }
}