using SpaceInvadersMob.Infrastructure.Containers;
using SpaceInvadersMob.UI;
using Zenject;

namespace SpaceInvadersMob.Infrastructure.States
{
    public class PauseGState: IGState
    {
        [Inject] private UIContainer _uiContainer;
        public void Enter(object data = null)
        {
            _uiContainer.GetForm<PauseForm>().Show();
        }

        public void Exit()
        {

        }
    }
}