using System;
using System.Linq;
using SpaceInvadersMob.Game.Weapons;
using SpaceInvadersMob.Infrastructure;
using SpaceInvadersMob.Infrastructure.Controllers;
using SpaceInvadersMob.Infrastructure.Factory;
using UniRx;
using UnityEngine;
using Zenject;

namespace SpaceInvadersMob.Game.Actors.Enemy
{
    
    public interface IEnemyView 
    {
        GameObject gameObject { get; }
        EnemyType EnemyType { get; }
        void AddReleaseCallback(Action<IEnemyView> release);
    }
    public abstract class BaseEnemy : MonoBehaviour, IDamage, IDamageValue, IEnemyView, IRuntimeObj
    {
        [Inject] private LootFactory  _lootFactory;
        [Inject] private DiContainer _diContainer;
        [Inject] private IGameRuntimeController  _gameRuntimeController;
        [SerializeField, Range(0.1f, 1000000f)]
        private float _life;

        [SerializeField, Range(0.1f, 1000000f)]
        private float _damage = 10f;

        [SerializeField, Range(0, 100)] private float _chanceLoot;
        private float _currentLife = 0;
        
        [SerializeField]
        private LootChance[] _lootChances;

        private Action<IEnemyView> _release;
     

        private void OnEnable()
        {
            _currentLife = _life;
            _lootChances = _lootChances.OrderByDescending(D => D.Chance).ToArray();
            StatAction();
        }

        private WeaponType CheckLoot()
        {
            if(_lootChances == null || _lootChances.Length == 0) return WeaponType.None;
            
            var randValue = UnityEngine.Random.Range(0, 100f);


            foreach (var item in _lootChances)
            {
                if (randValue < item.Chance) return item.WeaponType;
            }

            return WeaponType.None;
        }

        protected virtual void StatAction()
        {

        }

        protected virtual void DropLoot(WeaponType loot)
        {
            var obj = _lootFactory.Create(transform.position, loot);
            _diContainer.Inject(obj);
            _gameRuntimeController.AddObj(obj);

        }

        public void Damage(float value)
        {
 
            _currentLife -= value;

            if (_currentLife <= 0)
            {
                var loot = CheckLoot();
                if (loot != WeaponType.None) DropLoot(loot);
                MessageBroker.Default.Publish( MessageKillEnemy.Create(EnemyType));
                _release?.Invoke(this);
                _release = null;
            }
        }

        public float DamageValue => _damage;
        public abstract EnemyType EnemyType { get; }
        public void AddReleaseCallback(Action<IEnemyView> release)
        {
            _release = release;
        }
        
        public void Release()
        {
            _release?.Invoke(this);
            _release = null;
        }
        [Serializable]
        private class LootChance
        {
            [SerializeField, Range(0, 100)] 
            private float _chance;
            [SerializeField] 
            private WeaponType _weaponType;

            public float Chance => _chance;
            public WeaponType WeaponType => _weaponType;
        }


   
    }
}