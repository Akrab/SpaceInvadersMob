using SpaceInvadersMob.Game.Actors.Enemy;
using SpaceInvadersMob.Services;
using UnityEngine;
using Zenject;

namespace SpaceInvadersMob.Infrastructure.Factory
{
    public class EnemyFactory : PlaceholderFactory<Vector3, EnemyType, IEnemyView>
    {
        
    }

    public class CustomEnemyFactory : IFactory<Vector3, EnemyType, IEnemyView>
    {
        [Inject] private IGameResourceService _gameResourceService;
        [Inject] private DiContainer _diContainer;
        public IEnemyView Create(Vector3 pos, EnemyType enemyType)
        {
            var prefab =_gameResourceService.LoadEnemy(enemyType);
            var view = Object.Instantiate(prefab, pos, Quaternion.identity).GetComponent<IEnemyView>();
            _diContainer.Inject(view);
            return view;
        }
    }
}