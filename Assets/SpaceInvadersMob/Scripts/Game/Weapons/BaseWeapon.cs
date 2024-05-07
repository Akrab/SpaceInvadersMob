using System;
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
        // [SerializeField, Range(0.1f, 10f)] private float _fireRate = 0.5f;
        protected Transform[] _spawnPoints;
        private CompositeDisposable _fireDisposables = new CompositeDisposable();
        
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
            Observable.Timer(System.TimeSpan.FromSeconds(1f))
                .Repeat()
                .Subscribe(_ => { CreateProjectile(); }).AddTo(_fireDisposables);
        }

        public void Stop()
        {
            if (_fireDisposables != null)
                _fireDisposables.Dispose();
        }
    }
}