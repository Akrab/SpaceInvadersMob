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
        
        public IEnemyView Create(Vector3 pos, EnemyType enemyType)
        {
            var prefab =_gameResourceService.LoadEnemy(enemyType);
            return Object.Instantiate(prefab, pos, Quaternion.identity).GetComponent<IEnemyView>();
        }
    }
}