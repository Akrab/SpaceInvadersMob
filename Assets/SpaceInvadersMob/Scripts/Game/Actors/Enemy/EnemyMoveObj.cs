using UnityEngine;

namespace SpaceInvadersMob.Game.Actors.Enemy
{
    public class EnemyMoveObj :  BaseEnemy
    {
        [SerializeField]
        private float _moveSpeed = 3f;

        private bool _toRightMove = true;
        
        public override EnemyType EnemyType => EnemyType.Red;
        protected override void StatAction()
        {

            
        }
    }
}