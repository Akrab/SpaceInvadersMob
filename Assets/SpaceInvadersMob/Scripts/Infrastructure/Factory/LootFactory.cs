using SpaceInvadersMob.Game.Weapons;
using SpaceInvadersMob.Services;
using UnityEngine;
using Zenject;

namespace SpaceInvadersMob.Infrastructure.Factory
{

    
    public class LootFactory : PlaceholderFactory<Vector3, WeaponType, WeaponLootView>
    {
    }

    public class CustomLootFactory : IFactory<Vector3, WeaponType,  WeaponLootView>
    {
         [Inject] private IGameResourceService _gameResourceService;

        public WeaponLootView Create(Vector3 pos, WeaponType wType)
        {
            var prefab = _gameResourceService.LoadLootView(wType);
            return Object.Instantiate(prefab, pos, Quaternion.identity).GetComponent<WeaponLootView>();
        }
    }
}