using UnityEngine;

namespace SpaceInvadersMob.Game.Actors.Enemy
{
    public class EnemyMoveObj :  BaseEnemy
    {
        [SerializeField]
        private float _moveSpeed = 3f;
        private Camera _camera;
        protected override void StatAction()
        {
            if(_camera == null) _camera = Camera.main;
            
            
            
        }
    }
}