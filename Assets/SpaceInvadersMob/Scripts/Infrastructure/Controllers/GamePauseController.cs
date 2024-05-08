using SpaceInvadersMob.Infrastructure.Containers;
using SpaceInvadersMob.UI;
using UniRx;
using Zenject;

namespace SpaceInvadersMob.Infrastructure.Controllers
{
    public class GamePauseController
    {
        [Inject] private UIContainer _uiContainer;
        [Inject] private GameTickable _gameTickable;
        
        private CompositeDisposable _disposables;
        
        public GamePauseController()
        {
            _disposables = new CompositeDisposable();
            MessageBroker.Default
                .Receive<MessageOnPauseGame>() 
                .Subscribe(msg => {
                    if (msg.Data)
                    {
                        _uiContainer.GetForm<PauseForm>().Show();
                        _gameTickable.Stop();
                        return;
                    }
                    _uiContainer.GetForm<PauseForm>().Hide();
                    
                    _gameTickable.Run();
                }).AddTo (_disposables);
        }

        ~GamePauseController()
        {
            if(_disposables != null) _disposables.Dispose();
        }
    }
}