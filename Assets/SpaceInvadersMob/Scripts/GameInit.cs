using SpaceInvadersMob.Game;
using SpaceInvadersMob.Infrastructure.Controllers;
using SpaceInvadersMob.Infrastructure.Factory;
using SpaceInvadersMob.Infrastructure.Installers.Game;
using UniRx;
using UnityEngine;
using Zenject;

namespace SpaceInvadersMob.Infrastructure.Installers
{
    public class GameInit : MonoBehaviour
    {
        [SerializeField] private Transform _finishPoint;
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private Camera _camera;
        [SerializeField] private Vector3 _initCameraPos;
        [SerializeField] private CameraMove _cameraMove;

        [SerializeField] private EnemyWave[] _enemyWaves;
        [Inject] private GameRuntimeController _gameRuntimeController;
        [Inject] private PlayerFactory _customPlayerFactory;
        [Inject] private DiContainer _container;
        [Inject] private GameTickable _gameTickable;
        private Bounds _bounds;
        private int _waveIndex = 0;
        private CompositeDisposable _lifeDisposable;
        private void Tick()
        {
            _bounds = _camera.OrthographicBounds();
            if (_waveIndex >= _enemyWaves.Length)
            {
                if(_enemyWaves[^1].transform.position.y <=_bounds.max.y)
                {
                    _lifeDisposable?.Dispose();
                    MessageBroker.Default.Publish( MessageOnGameEnd.Create());
                }
                
                return;
            }
   
            if (_enemyWaves[_waveIndex].transform.position.y - _bounds.max.y < 0.5f)
            {
                _container.Inject(_enemyWaves[_waveIndex]);
                _enemyWaves[_waveIndex].Create();
                _waveIndex++;
            }
        }

        public void Create()
        {
            _lifeDisposable?.Dispose();
            _waveIndex = 0;
            _camera.transform.position = _initCameraPos;


            foreach (var item in _enemyWaves)
            {
                _gameRuntimeController.AddEnemy(item.Count);
            }
            
            
            var obj = _customPlayerFactory.Create(_playerSpawnPoint.position);
            _gameRuntimeController.AddObj(obj);
            _container.Inject(obj);
            _container.Inject(_cameraMove);
            _gameTickable.Run();
            _lifeDisposable = new CompositeDisposable();
            _gameTickable.ReactiveCommand.Subscribe(_ => Tick()).AddTo(_lifeDisposable);
        }

    }
}
