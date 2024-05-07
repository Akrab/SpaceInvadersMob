using SpaceInvadersMob.Infrastructure.Containers;
using SpaceInvadersMob.UI;
using Zenject;

namespace SpaceInvadersMob.Infrastructure.States
{
    public class MainMenuGState : IGState
    {
        [Inject] private readonly UIContainer _uiContainer;
        public void Enter(object data = null)
        {
            _uiContainer.GetForm<MainForm>().Show();
        }

        public void Exit()
        {
            _uiContainer.GetForm<MainForm>().Hide();
        }
    }
}
