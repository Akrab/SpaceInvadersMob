using SpaceInvadersMob.Game.Actors.Player;
using SpaceInvadersMob.Services;

using UnityEngine;
using Zenject;

namespace SpaceInvadersMob.Infrastructure.Factory
{
    public class PlayerFactory : PlaceholderFactory<Vector3, IPlayerView>
    {
    }

    public class CustomPlayerFactory : IFactory<Vector3,  IPlayerView>
    {

        [Inject] private IGameResourceService _gameResourceService;

        public IPlayerView Create(Vector3 param)
        {
            var pl = _gameResourceService.LoadPlayer();
            return Object.Instantiate(pl, param, Quaternion.identity).GetComponent<IPlayerView>();
        }
    }
}