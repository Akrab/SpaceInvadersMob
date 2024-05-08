using System;
using System.Collections.Generic;
using SpaceInvadersMob.Game.Actors.Enemy;
using SpaceInvadersMob.Infrastructure.Controllers;
using SpaceInvadersMob.Infrastructure.Factory;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;
using Object = UnityEngine.Object;


namespace SpaceInvadersMob.Infrastructure.Pools
{
    public class EnemyPool
    {
        [Inject] private EnemyFactory _enemyFactory;
        [Inject] private IGameRuntimeController _gameRuntimeController;
        
        private Dictionary<EnemyType, ObjectPool<IEnemyView>>
            _pools = new Dictionary<EnemyType, ObjectPool<IEnemyView>>();


        public EnemyPool()
        {
            var v = (EnemyType[])Enum.GetValues(typeof(EnemyType));
            foreach (var item in v)
            {
                if (item == EnemyType.None) continue;

                var pool = new ObjectPool<IEnemyView>(
                    () => CreateFunc(item),
                    view => view.gameObject.SetActive(true),
                    view => view.gameObject.SetActive(false),
                    view => Object.Destroy(view.gameObject),
                    false, 5, 20);
                _pools.Add(item, pool);
            }
        }

        private IEnemyView CreateFunc(EnemyType enemyType)
        {
            var newEnemy = _enemyFactory.Create(Vector3.zero, enemyType);
            return newEnemy;
        }

        public IEnemyView Get(EnemyType enemyType)
        {
            var e =  _pools[enemyType].Get();
            _gameRuntimeController.AddObj(e as IRuntimeObj);
            e.AddReleaseCallback(Release);
            return e;
        }

        public void Release(IEnemyView view)
        {
            _pools[view.EnemyType].Release(view);
        }
        
        
    }
}