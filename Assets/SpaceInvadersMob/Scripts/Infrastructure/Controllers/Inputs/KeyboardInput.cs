using UnityEngine;

namespace SpaceInvadersMob.Infrastructure.Controllers.Inputs
{
    public class KeyboardInput : IInput
    {
        private const string INPUT_HORIZONTAL = "Horizontal";
        
        public float Get()
        { 
            return Input.GetAxisRaw(INPUT_HORIZONTAL);
        }
    }
}