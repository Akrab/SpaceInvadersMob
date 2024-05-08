using SpaceInvadersMob.Infrastructure.Containers;
using SpaceInvadersMob.Infrastructure.Controllers;
using SpaceInvadersMob.UI;
using UniRx;
using UnityEngine.SceneManagement;
using Zenject;

namespace SpaceInvadersMob.Infrastructure.States
{
    public class GameGState : IGState
    {
        [Inject] private UIContainer _uiContainer;
       
        private bool Loaded(float value)
        {
            _uiContainer.GetForm<GameForm>().Show();

            return true;
        }

        public void Enter(object data = null)
        {
            SceneManager.LoadSceneAsync(CONSTANTS.GAME_SCENE, LoadSceneMode.Additive)
                .ToObservable()
                .Last(Loaded)
                
                .Subscribe();

        }

        public void Exit()
        {
            _uiContainer.GetForm<GameForm>().Hide();
        }

    }
}