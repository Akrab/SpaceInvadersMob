using SpaceInvadersMob.Game.Actors.Enemy;
using SpaceInvadersMob.Game.Projectiles;
using SpaceInvadersMob.Game.Weapons;
using UnityEngine;

namespace SpaceInvadersMob.Services
{
    public interface IGameResourceService
    {
        GameObject LoadPlayer();
        GameObject LoadEnemy(EnemyType enemyType);
        GameObject LoadProjectile(ProjectileType target);

        GameObject LoadLootView(WeaponType weaponType);

    }
}