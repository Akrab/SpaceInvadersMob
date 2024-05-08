using SpaceInvadersMob.Infrastructure.Containers;
using SpaceInvadersMob.UI;
using UniRx;
using Zenject;

namespace SpaceInvadersMob.Infrastructure.Controllers
{
    public class GameEndController
    {
        [Inject] private GameCreatorController _gameCreatorController;
        [Inject] private GameRuntimeController _runtimeController;
        [Inject] private GameTickable _gameTickable;
        [Inject] private UIContainer _uiContainer;
        private CompositeDisposable _disposables;

        private void GameEnd()
        {

            _gameTickable.Stop();
            _runtimeController.ReleaseAll();
            _uiContainer.GetForm<EndGameForm>().Show();
        }

        public GameEndController()
        {
            _disposables = new CompositeDisposable();
            MessageBroker.Default
                .Receive<MessageOnGameEnd>()
                .Subscribe(msg => GameEnd()).AddTo(_disposables);

            MessageBroker.Default
                .Receive<MessageKillEnemy>()
                .Subscribe(msg =>
                {
                    _runtimeController.DecEnemy();
                    if (_runtimeController.CountEnemy == 0) MessageBroker.Default.Publish(MessageOnGameEnd.Create());
                }).AddTo(_disposables);


        }

        ~GameEndController()
        {
            _disposables?.Dispose();
        }
    }
}