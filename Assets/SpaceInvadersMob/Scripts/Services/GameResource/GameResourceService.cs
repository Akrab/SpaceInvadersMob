using System;
using SpaceInvadersMob.Game.Actors.Enemy;
using SpaceInvadersMob.Game.Projectiles;
using SpaceInvadersMob.Game.Weapons;
using UnityEngine;

namespace SpaceInvadersMob.Services
{
    public class GameResourceService : IGameResourceService
    {
        private T LoadPrefab<T>(string path) where T : UnityEngine.Object
        {
            return Resources.Load<T>(path);
        }

        public GameObject LoadPlayer()
        {
            return LoadPrefab<GameObject>(CONSTANTS.PLAYER_SHIP_PREFAB);
        }

        public GameObject LoadEnemy(EnemyType target)
        {
            return LoadPrefab<GameObject>(
                $"{CONSTANTS.ENEMY_PREFAB}{Enum.GetName(typeof(EnemyType), target)}");
        }

        public GameObject LoadProjectile(ProjectileType target)
        {
            return LoadPrefab<GameObject>(
                $"{CONSTANTS.PROJECTILE_PREFAB}{Enum.GetName(typeof(ProjectileType), target)}");
        }

        public GameObject LoadLootView(WeaponType target)
        {
            return LoadPrefab<GameObject>(
                $"{CONSTANTS.LOOT_PREFAB}{Enum.GetName(typeof(WeaponType), target)}");
        }
    }
}