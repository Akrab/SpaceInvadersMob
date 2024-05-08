using SpaceInvadersMob.Game.Weapons;
using SpaceInvadersMob.Infrastructure;
using SpaceInvadersMob.Infrastructure.Containers;
using SpaceInvadersMob.Infrastructure.Controllers;
using SpaceInvadersMob.Infrastructure.DataModel;
using SpaceInvadersMob.Infrastructure.Pools;
using UniRx;
using UnityEngine;
using Zenject;

namespace SpaceInvadersMob.Game.Actors.Player
{

    public interface IPlayerView :IRuntimeObj
    {
        GameObject gameObject { get; }
    }

    public class PlayerObj : MonoBehaviour, IPlayerView, IDamage
    {

        [SerializeField] private BoxCollider2D _boxCollider2D;
        [SerializeField] private Transform[] _projectileSpawnPoints;

        [SerializeField] private float _life = 5f;
        
        [Inject] private ProjectileLinePool _projectileLinePool;
        [Inject] private IConfigContainer _configContainer;
        [Inject] private IGameStateMachine _gameStateMachine;
        [Inject] private DiContainer _diContainer;
        [Inject] private GameTickable _gameTickable;
        
        private Camera _camera;
        private Bounds _bounds;
        private IWeapon _weapon;
        private Vector3 _newPos = Vector3.zero;
        private float _currentLife = 0;
        
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
            _verticalSpeed = cc.Speed.y;
            
            SetWeapon(WeaponType.Base);
            _gameTickable.ReactiveCommand.Subscribe(_=>Move()).AddTo(this);
            
        }

        private void SetWeapon(WeaponType weaponType)
        {
            BaseWeapon bw = null;
            switch (weaponType)
            {
                case WeaponType.Base:
                    bw = new MainWeapon();

                    break;
                case WeaponType.Mega:
                    bw = new MegaWeapon();
                    break;
                case WeaponType.Epic:
                    bw = new EpicWeapon();

                    break;
                default: return;
            }
            
            if (_weapon != null) _weapon.Stop();
            
            bw.SetPoints(_projectileSpawnPoints);
            _diContainer.Inject(bw);
            _weapon = bw;
            _weapon.Fire();
        }

        private void Move()
        {
            var size = _boxCollider2D.size.x * transform.lossyScale.x;

            var leftPos = _bounds.min.x + size / 2f;
            var rightPos = _bounds.max.x - size / 2f;

            _newPos = Vector3.zero;

            Vector3 direction = Vector3.zero;

            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved)
                {
                    var tPos = _camera.ScreenToWorldPoint(touch.position);
                    direction = (tPos - transform.position);
                }
            }

            direction.z = 0;
            direction.y = _verticalSpeed * Time.deltaTime;
            transform.Translate(direction);
            _newPos = transform.position;
            _newPos.x = Mathf.Clamp(transform.position.x, leftPos, rightPos);
            transform.position = _newPos;

        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            CheckEnemy(other);
            CheckLoot(other);
        }

        private void CheckEnemy(Collision2D other)
        {
            if (!other.transform.CompareTag(CONSTANTS.ENEMY_TAG)) return;
            var dmV = other.transform.GetComponent<IDamageValue>();
            if (dmV != null)
                Damage(dmV.DamageValue);
        }

        private void CheckLoot(Collision2D other)
        {
            if (!other.transform.CompareTag(CONSTANTS.WEAPON_LOOT_TAG)) return;
            var wlv = other.transform.GetComponent<WeaponLootView>();
            if (wlv != null)
                SetWeapon(wlv.WeaponType);
            
            Destroy(other.transform.gameObject);


        }

        public void Damage(float value)
        {
            _currentLife -= value;
            if (_currentLife <= 0)
            {
                MessageBroker.Default.Publish( MessageOnGameEnd.Create());
            }
        }

        public void Release()
        {
            _weapon?.Stop();
            Destroy(gameObject);
        }
    }
}