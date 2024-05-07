using SpaceInvadersMob.Infrastructure.Pools;
using UnityEngine;
using Zenject;

namespace SpaceInvadersMob.Game.Weapons
{
    public class MainWeapon : BaseWeapon
    {
        public ProjectileLinePool _projectileLinePool;
        
        protected override void CreateProjectile()
        {
            _projectileLinePool.Get().transform.position = _spawnPoints[0].position;
        }
    }
}