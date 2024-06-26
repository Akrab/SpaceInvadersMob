﻿using SpaceInvadersMob.Infrastructure.Pools;
using Zenject;

namespace SpaceInvadersMob.Game.Weapons
{
    public class MainWeapon : BaseWeapon
    {
        [Inject]
        private ProjectileLinePool _projectileLinePool;
        
        protected override void CreateProjectile()
        {
            _projectileLinePool.Get().transform.position = _spawnPoints[0].position;
        }
    }
}