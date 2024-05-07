using UnityEngine;

namespace SpaceInvadersMob.Infrastructure.DataModel
{

    [CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObject/New GameConfig")]
    public class GameConfigModel : ScriptableObject
    {
        [SerializeField] private Vector2 _speed;

        public Vector2 Speed => _speed;
    }
}