using System;
using System.Collections;
using System.Collections.Generic;
using SpaceInvadersMob.Infrastructure.Controllers;
using UnityEngine;
using Zenject;

namespace SpaceInvadersMob.Game.Weapons
{
    public class WeaponLootView : MonoBehaviour, IRuntimeObj
    {
        [Inject] private IGameRuntimeController _gameRuntimeController;
        [SerializeField] private WeaponType _weaponType;

        public WeaponType WeaponType => _weaponType;

        public void Release()
        {
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            _gameRuntimeController.RemoveObj(this);
        }
    }
}
