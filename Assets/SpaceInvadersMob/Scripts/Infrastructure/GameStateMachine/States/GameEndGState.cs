using SpaceInvadersMob.Infrastructure.Containers;
using SpaceInvadersMob.UI;
using Zenject;

namespace SpaceInvadersMob.Infrastructure.States
{
    public class GameEndGState : IGState
    {
        [Inject] private UIContainer _uiContainer;
        public void Enter(object data = null)
        {
            _uiContainer.GetForm<EndGameForm>().Show();
        }

        public void Exit()
        {

        }
    }
}