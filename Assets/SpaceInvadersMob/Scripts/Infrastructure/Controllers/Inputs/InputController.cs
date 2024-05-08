using SpaceInvadersMob.Infrastructure.Controllers.Inputs;
using UniRx;
using UnityEngine.Events;
using Zenject;

namespace SpaceInvadersMob.Infrastructure.Controllers
{

    public class MoveInputEvent : UnityEvent<float>
    {
    }

    public interface IInputController
    {
        float MoveInputEvent { get; }

    }

    public class InputController : IInputController, ITickable
    {

        private IInput _input;

        private float _moveInputEvent;

        public float MoveInputEvent => _moveInputEvent;

        public InputController()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            _input = new KeyboardInput();
#endif

#if UNITY_ANDROID && UNITY_EDITOR
            _input = new TouchInput();
#endif

        }

        public void Tick()
        {
            _moveInputEvent = _input.Get();
        }

    }
}