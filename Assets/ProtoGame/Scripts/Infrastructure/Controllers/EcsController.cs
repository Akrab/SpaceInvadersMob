using Leopotam.EcsLite;
using System;
using Zenject;

namespace ProtoGame.Infrastructure.Controllers
{
    public class EcsController : ITickable, IInitializable, IDisposable
    {

        [Inject] private readonly EcsWorld _ecsWorld;
        [Inject] private readonly DiContainer _container;

        private EcsSystems _updateSystem;

        public bool IsInit { get;  set; }
        public bool IsRunGame { get; set; }
        public void Dispose()
        {
            _updateSystem?.Destroy();
        }

        public void Initialize()
        {
            _updateSystem = new EcsSystems(_ecsWorld);


#if UNITY_EDITOR
            // Регистрируем отладочные системы по контролю за состоянием каждого отдельного мира:
            // .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ("events"))
            _updateSystem.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem());
            // Регистрируем отладочные системы по контролю за текущей группой систем. 
            _updateSystem.Add(new Leopotam.EcsLite.UnityEditor.EcsSystemsDebugSystem());
#endif
          //  _updateSystem.Inject();
            _updateSystem.Init();
            IsInit = true;
        }

        public void Tick()
        {
            if (!IsInit) return;
            if (!IsRunGame) return;
            _updateSystem?.Run();
        }


    }
}
