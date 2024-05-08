using System.Collections.Generic;
using System.Linq;
using SpaceInvadersMob.Game.Actors.Enemy;
using SpaceInvadersMob.Game.Weapons;
using UniRx;
using UnityEngine;
using Zenject;

namespace SpaceInvadersMob.Infrastructure.Controllers
{

    public interface IRuntimeObj
    {
        GameObject gameObject { get; }
        void Release();
    }

    public interface IGameRuntimeController
    {
        void AddObj(IRuntimeObj obj);
        void AddEnemy(int count);
        void DecEnemy();

        void RemoveObj(IRuntimeObj obj);
        ReactiveDictionary<EnemyType, int> KillCounter { get; }

    }

    public class GameRuntimeController : IGameRuntimeController
    {

        private Bounds _bounds;
        private List<IRuntimeObj> _objs = new List<IRuntimeObj>(100);

        private ReactiveDictionary<EnemyType, int> _killCounter = new();
        private int _countEnemy = 0;
        private CompositeDisposable _disposables = new ();
        public int CountEnemy => _countEnemy;
        public ReactiveDictionary<EnemyType, int> KillCounter => _killCounter;

        private GameRuntimeController()
        {
            _killCounter.Add(EnemyType.Base, 0);
            _killCounter.Add(EnemyType.Red, 0);
            
            MessageBroker.Default
                .Receive<MessageKillEnemy>() 
                .Subscribe(msg => {
                    _killCounter[msg.Data]++;
 
                }).AddTo (_disposables);
        }

        public void AddObj(IRuntimeObj obj)
        {
            if (_objs.Contains(obj)) return;
            _objs.Add(obj);
        }

        public void AddEnemy(int count)
        {
            _countEnemy+= count;
        }

        public void DecEnemy()
        {
            _countEnemy--;
        }

        public void RemoveObj(IRuntimeObj obj)
        {
            _objs.Remove(obj);
        }

        public void ReleaseAll()
        {
            for (int i = 0; i < _objs.Count; i++)
                _objs[i].Release();
            _objs.Clear();
            _countEnemy = 0;

            var keys = _killCounter.Keys.ToArray();


            foreach (var key in keys)
            {
                _killCounter[key] = 0;
            }
        }

        ~GameRuntimeController()
        {
            _disposables?.Dispose();
        }

    }
}