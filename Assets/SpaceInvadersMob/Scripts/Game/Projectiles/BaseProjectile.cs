using System;
using SpaceInvadersMob.Game.Actors;
using UniRx;
using UnityEngine;

namespace SpaceInvadersMob.Game.Projectiles
{
    public abstract class BaseProjectile<T> : MonoBehaviour where T : class
    {
        
        [SerializeField, Range(1, 100)] private float _speed;
        [SerializeField, Range(0.1f, 1000)] private float _damage;
        private Action<T> _onReturn;      
        private CompositeDisposable _lifeDisposable;

        protected void OnEnable()
        {
            _lifeDisposable = new CompositeDisposable();
            Observable.EveryUpdate().Subscribe(Move).AddTo(_lifeDisposable);
            Observable.Timer(System.TimeSpan.FromSeconds(1))
                .Subscribe(_ => { Dispose(); }).AddTo(_lifeDisposable);
        }

        private void Dispose()
        {
            _lifeDisposable.Dispose();
            _onReturn.Invoke(this as T);
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
        
        protected virtual void Move(long t)
        {
            transform.Translate(Vector2.up * _speed * Time.deltaTime );
        }

        public void OnReturn(Action<T> onReturn)
        {
            _onReturn = onReturn;
        }

    }
}