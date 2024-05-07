using System;
using SpaceInvadersMob.Game.Actors.Enemy;
using SpaceInvadersMob.Infrastructure.Pools;
using UnityEngine;
using Zenject;

namespace SpaceInvadersMob.Game
{
    public interface IEnemyWave
    {
        void Create();
    }
    public class EnemyWave : MonoBehaviour, IEnemyWave
    {

        [SerializeField] private EnemyPoint[] _points;

        [Inject] private EnemyPool _enemyPool;

        public void Create()
        {
            foreach (var point in _points)
                _enemyPool.Get(point.EnemyType).gameObject.transform.position = point.Position;

        }
        
        [Serializable]
        private class EnemyPoint
        {
            [SerializeField] private Transform _point;
            [SerializeField] private EnemyType _enemyType;

            public Vector3 Position => _point.position;
            public EnemyType EnemyType => _enemyType;
        }
    }

  
}
