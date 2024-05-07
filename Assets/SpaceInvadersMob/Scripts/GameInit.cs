using System;
using SpaceInvadersMob.Game;
using SpaceInvadersMob.Infrastructure.Factory;
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
        
        [SerializeField] private EnemyWave[] _enemyWaves;
        [Inject] private PlayerFactory _customPlayerFactory;
        [Inject] private DiContainer _container;

        private Bounds _bounds;
        private int _waveIndex = 0;

        private void Update()
        {
            if (_waveIndex >= _enemyWaves.Length) return;
            
            if (_enemyWaves[_waveIndex].transform.position.y - _bounds.max.y < 2f)
            {
                _container.Inject(_enemyWaves[_waveIndex]);
                _enemyWaves[_waveIndex].Create();
                _waveIndex++;
            }
        }

        public void Create()
        {
            _waveIndex = 0;
            _camera.transform.position = _initCameraPos;
            var obj = _customPlayerFactory.Create(_playerSpawnPoint.position);
            _container.Inject(obj);
            _bounds = _camera.OrthographicBounds();
        }
        
    }
}
