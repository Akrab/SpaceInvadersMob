using SpaceInvadersMob.Infrastructure.Containers;
using SpaceInvadersMob.Infrastructure.Controllers;
using SpaceInvadersMob.Infrastructure.DataModel;
using UniRx;
using UnityEngine;
using Zenject;

namespace SpaceInvadersMob.Infrastructure.Installers.Game
{
    public class CameraMove : MonoBehaviour
    {
        [Inject] private IConfigContainer _configContainer;
        [Inject] private GameTickable _gameTickable;
        private float _speed = 1;
        private Vector3 _newPos = Vector3.zero;
        private CompositeDisposable _lifeDisposable;
        [Inject]
        private void Install()
        {
            var config = _configContainer.Get<GameConfigModel>();
            _speed = config.Speed.y;

            _lifeDisposable?.Dispose();
            _lifeDisposable = new CompositeDisposable();
           _gameTickable.ReactiveCommand.Subscribe(_ => Move()).AddTo(_lifeDisposable);

        }

        private void Move()
        {
            _newPos = Vector3.zero;
            _newPos.y = _speed * Time.deltaTime;
            transform.Translate(_newPos);
        }

        private void OnDestroy()
        {
            _lifeDisposable?.Dispose();
        }
    }
}