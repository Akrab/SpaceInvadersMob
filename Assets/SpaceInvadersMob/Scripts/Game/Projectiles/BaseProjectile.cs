using System;
using SpaceInvadersMob.Game.Actors;
using SpaceInvadersMob.Infrastructure.Controllers;
using UniRx;
using UnityEngine;
using Zenject;

namespace SpaceInvadersMob.Game.Projectiles
{
    public abstract class BaseProjectile<T> : MonoBehaviour, IRuntimeObj where T : class
    {
        [Inject] private GameTickable _gameTickable;
        
        [SerializeField, Range(1, 100)] private float _speed;
        [SerializeField, Range(0.1f, 1000)] private float _damage;
        
        private Action<T> _onRelease;      
        private CompositeDisposable _lifeDisposable;
        private Camera _camera;
        
        private void Dispose()
        {
            _lifeDisposable.Dispose();
            _onRelease?.Invoke(this as T);
        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.transform.CompareTag(CONSTANTS.ENEMY_TAG))
            {
                var damage = other.transform.GetComponent<IDamage>();
                damage?.Damage(_damage);
                Dispose();
            }
        }
        
        protected virtual void Move()
        {
            transform.Translate(Vector2.up * _speed * Time.deltaTime );
            if(transform.position.y > _camera.OrthographicBounds().max.y + 2f)  Dispose();
        }

        public void OnRelease(Action<T> onRelease)
        {
            _onRelease = onRelease;
        }

        public void OnFire()
        {
            _camera = Camera.main;
            if (_lifeDisposable != null)
                _lifeDisposable.Dispose();

            _lifeDisposable = new CompositeDisposable();
            _gameTickable.ReactiveCommand.Subscribe(_ => Move()).AddTo(_lifeDisposable);
        
        }

        public void Release()
        {
            Dispose();
        }
    }
}