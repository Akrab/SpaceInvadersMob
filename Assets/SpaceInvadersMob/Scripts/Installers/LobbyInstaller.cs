using SpaceInvadersMob.Infrastructure.States;
using Zenject;

namespace SpaceInvadersMob.Infrastructure.Installers
{
    public class LobbyInstaller : MonoInstaller<LobbyInstaller>, IInitializable
    {
        [Inject] private IGameStateMachine _gameStateMachine;
        public override void InstallBindings()
        {
            _gameStateMachine.EnterToState<LoadingGState>();
            Container.BindInterfacesAndSelfTo<LobbyInstaller>().FromInstance(this).AsSingle();
        }
        
        public void Initialize()
        {
            Container.Resolve<IGameStateMachine>().EnterToState<MainMenuGState>();
        }

    }
}