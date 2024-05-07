using UnityEngine;

namespace SpaceInvadersMob.Infrastructure.Controllers.Inputs
{
    public class TouchInput : IInput
    {
        // private Vector2 _touchPos;
        // private Touch _touch;
        
        public float Get()
        {
            // if (Input.touchCount > 0)
            // {
            //     _touch = Input.GetTouch(0);
            //
            //     if (_touch.phase == TouchPhase.Began)
            //     {
            //         _touchPos = _touch.position;
            //     }
            //     else if (_touch.phase == TouchPhase.Moved || _touch.phase == TouchPhase.Ended)
            //
            //     {
            //         
            //     }
            // }
            //
            // _touchPos = Vector2.zero;
            return Input.GetAxis("Mouse X");
        }
    }
}