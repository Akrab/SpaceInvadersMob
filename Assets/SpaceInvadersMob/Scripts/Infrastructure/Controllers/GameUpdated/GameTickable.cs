using UniRx;
using Zenject;

namespace SpaceInvadersMob.Infrastructure.Controllers
{
    
    public interface IGameTickable
    {
        ReactiveCommand  ReactiveCommand { get; }
    }
    
    public class GameTickable : IGameTickable, ITickable
    {
        
        private ReactiveCommand  _reactiveCommand = new ();
        
        private bool _tick = false;

        public ReactiveCommand ReactiveCommand => _reactiveCommand;
        
        public void Run()
        {
            _tick = true;
        }

        public void Stop()
        {
            _tick = false;
        }
        
        public void Tick()
        {
           if (!_tick) return;

           _reactiveCommand.Execute();
        }



    }
}
