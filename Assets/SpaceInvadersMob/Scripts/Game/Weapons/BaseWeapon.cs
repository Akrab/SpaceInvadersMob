using SpaceInvadersMob.Infrastructure;
using UniRx;
using UnityEngine;

namespace SpaceInvadersMob.Game.Weapons
{
    public interface IWeapon
    {
        void Fire();
        void Stop();
    }

    public abstract class BaseWeapon : IWeapon
    {
        [SerializeField] private WeaponType _weaponType;
        private CompositeDisposable _fireDisposables;
        protected bool _isSkip = false;
        protected Transform[] _spawnPoints;
        protected virtual void CreateProjectile()
        {

        }
        

        public void SetPoints(Transform[] spawnPoints)
        {
            _spawnPoints = spawnPoints;
        }

        public void Fire()
        {
            Stop();
            _fireDisposables = new CompositeDisposable();
            
            MessageBroker.Default
                .Receive<MessageOnPauseGame>() 
                .Subscribe(msg =>
                {
                    _isSkip = msg.Data;
                }).AddTo (_fireDisposables);
        
            Observable.Timer(System.TimeSpan.FromSeconds(1f))
                .Repeat()
                .Subscribe(_ =>
                {
                    if(_isSkip) return;
                    CreateProjectile();
                }).AddTo(_fireDisposables);
        }

        public void Stop()
        {
            if (_fireDisposables != null)
                _fireDisposables.Dispose();
        }
    }
}