using UnityEngine;

namespace SpaceInvadersMob.Infrastructure.Controllers.Inputs
{
    public class TouchInput : IInput
    {
        private Vector2 _touchStartPos;
        
        private Touch _touch;

        public float Get()
        {
            if (Input.touchCount > 0)
            {
                _touch = Input.GetTouch(0);

                if (_touch.phase == TouchPhase.Began)
                {
                    _touchStartPos = _touch.position;
                }
                else if (_touch.phase == TouchPhase.Moved || _touch.phase == TouchPhase.Ended)
                {
                 
                    return _touch.deltaPosition.x;
                    //var x = _touch.position.x - _touchStartPos.x;
                    // if (Mathf.Abs(x) > 0)
                    // {
                    //     return x > 0 ? 1 : -1;
                    // }
                }
            }

            return 0;
        }
    }
}