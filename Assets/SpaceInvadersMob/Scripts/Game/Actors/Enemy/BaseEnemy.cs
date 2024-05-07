using System;
using SpaceInvadersMob.Game.Weapons;
using UnityEngine;

namespace SpaceInvadersMob.Game.Actors.Enemy
{
    
    public interface IEnemyView
    {
        GameObject gameObject { get; }
        EnemyType EnemyType { get; }
        void AddReleaseCallback(Action<IEnemyView> release);
    }
    public abstract class BaseEnemy : MonoBehaviour, IDamage, IDamageValue, IEnemyView
    {
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
            StatAction();
        }

        private WeaponType CheckLoot()
        {
            if(_lootChances == null || _lootChances.Length == 0) return WeaponType.None;

            float sum = 0;
            
            foreach (var item in _lootChances)
                sum += item.Chance;

            var randValue = UnityEngine.Random.Range(0, sum);
            var total = 0.0f;
        
            foreach (var item in _lootChances)
            {
                total += item.Chance;
                if (total > randValue) return item.WeaponType;
            }

            return WeaponType.None;
        }

        protected virtual void StatAction()
        {

        }

        protected virtual void DropLoot(WeaponType loot)
        {

            
        }

        public void Damage(float value)
        {
            _currentLife -= value;

            if (_currentLife <= 0)
            {
                var loot = CheckLoot();
                if (CheckLoot() != WeaponType.None) DropLoot(loot);
                _release(this);
            }
        }

        public float DamageValue => _damage;
        public abstract EnemyType EnemyType { get; }
        public void AddReleaseCallback(Action<IEnemyView> release)
        {
            _release = release;
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