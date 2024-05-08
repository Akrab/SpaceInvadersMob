using UniRx;
using Zenject;

namespace SpaceInvadersMob.Infrastructure.Controllers
{
    public class GameRestartController
    {
        [Inject] private GameCreatorController _gameCreatorController;
        [Inject] private GameRuntimeController _runtimeController;
        private CompositeDisposable _disposables;
      
        private void RestartGame()
        {
            _runtimeController.ReleaseAll();
            _gameCreatorController.StartGame();
        }
        
        public GameRestartController()
        {
            _disposables = new CompositeDisposable();
            MessageBroker.Default
                .Receive<MessageRestartGame>()
                .Subscribe(msg => RestartGame()).AddTo(_disposables);
        }

        ~GameRestartController()
        {
            _disposables?.Dispose();
        }
    }
}