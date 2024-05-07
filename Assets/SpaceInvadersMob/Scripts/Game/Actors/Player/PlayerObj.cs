using SpaceInvadersMob.Game.Weapons;
using SpaceInvadersMob.Infrastructure.Containers;
using SpaceInvadersMob.Infrastructure.Controllers;
using SpaceInvadersMob.Infrastructure.DataModel;
using SpaceInvadersMob.Infrastructure.Pools;
using UniRx;
using UnityEngine;
using Zenject;

namespace SpaceInvadersMob.Game.Actors.Player
{

    public interface IPlayerView
    {
        GameObject gameObject { get; }
    }

    public class PlayerObj : MonoBehaviour, IPlayerView, IDamage
    {

        [SerializeField] private BoxCollider2D _boxCollider2D;

        [SerializeField] private Transform[] _projectileSpawnPoints;

        [SerializeField] private float _life = 5f;

        [Inject] private IInputController _inputController;
        [Inject] private ProjectileLinePool _projectileLinePool;
        [Inject] private IConfigContainer _configContainer;

        private Camera _camera;
        private Bounds _bounds;
        private IWeapon _weapon;
        private Vector3 _newPos = Vector3.zero;
        private float _currentLife = 0;

        private float _speed = 1;
        private float _verticalSpeed = 1;

        private void Start()
        {
            _camera = Camera.main;
            _bounds = _camera.OrthographicBounds();
            _currentLife = _life;
        }

        [Inject]
        private void Install()
        {
            var cc = _configContainer.Get<GameConfigModel>();
            _speed = cc.Speed.x;
            _verticalSpeed = cc.Speed.y;

            Observable.EveryUpdate().Subscribe(Move).AddTo(this);
            SetWeapon(WeaponType.Base);
        }

        private void SetWeapon(WeaponType weaponType)
        {
            var w = new MainWeapon
            {
                _projectileLinePool = _projectileLinePool
            };
            w.SetPoints(_projectileSpawnPoints);
            _weapon = w;
            _weapon.Fire();
        }

        private void Move(long time)
        {
            if (_camera == null) return;

            var size = _boxCollider2D.size.x * transform.lossyScale.x;

            var leftPos = _bounds.min.x + size / 2f;
            var rightPos = _bounds.max.x - size / 2f;

            _newPos = Vector3.zero;
            _newPos.y = _verticalSpeed * Time.deltaTime;
            _camera.transform.position += _camera.transform.TransformDirection(_newPos);

            _newPos = Vector3.zero;
            _newPos.x = Input.GetAxisRaw("Horizontal") * _speed * Time.deltaTime;

            _newPos.y = _verticalSpeed * Time.deltaTime;
            _newPos.z = 0;

            transform.position += transform.TransformDirection(_newPos);

            _newPos = transform.position;
            _newPos.x = Mathf.Clamp(transform.position.x, leftPos, rightPos);
            transform.position = _newPos;

            // transform.Translate(Vector2.one * Input.GetAxisRaw("Horizontal") * _speed * Time.deltaTime);
            //
            // _newPos = transform.position;
            // _newPos.x = Mathf.Clamp(transform.position.x, leftPos, rightPos);
            //
            // transform.position = _newPos;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.transform.CompareTag(CONSTANTS.ENEMY_TAG)) return;
            var dmV = other.transform.GetComponent<IDamageValue>();
            if (dmV != null)
                Damage(dmV.DamageValue);
        }

        public void Damage(float value)
        {
            _currentLife -= value;
            if (_currentLife <= 0)
                Destroy(gameObject);
        }
    }
}