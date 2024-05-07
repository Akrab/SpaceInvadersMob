using SpaceInvadersMob.Game.Actors.Enemy;
using SpaceInvadersMob.Game.Actors.Player;
using SpaceInvadersMob.Game.Weapons;
using SpaceInvadersMob.Infrastructure.Containers;
using SpaceInvadersMob.Infrastructure.Controllers;
using SpaceInvadersMob.Infrastructure.Controllers.Timers;
using SpaceInvadersMob.Infrastructure.Factory;
using SpaceInvadersMob.Infrastructure.Pools;
using SpaceInvadersMob.Infrastructure.States;
using SpaceInvadersMob.Services;
using UnityEngine;
using Zenject;


namespace SpaceInvadersMob.Infrastructure.Installers
{
    public class ProjectInstaller : MonoInstaller<ProjectInstaller>
    {
        [SerializeField] private ScriptableObject[] containers;
        
        private void InstallServices()
        {
            Container.BindInterfacesTo<GameResourceService>().FromNew().AsSingle().NonLazy();
            
        }

        private void InstallControllers()
        {
            Container.BindInterfacesAndSelfTo<TimerController>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameTickable>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesTo<InputController>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameCreatorController>().FromNew().AsSingle().NonLazy();
        }

        private void InstallPools()
        {
            Container.BindInterfacesAndSelfTo<ProjectileLinePool>().FromNew().AsSingle().WithArguments(2, 5).NonLazy();
            Container.BindInterfacesAndSelfTo<ProjectileSpherePool>().FromNew().AsSingle().WithArguments(2, 5).NonLazy();
        }

        private void InstallFabrics()
        {
            Container.BindFactory<Vector3, IPlayerView, PlayerFactory>().FromFactory<CustomPlayerFactory>().NonLazy();
            Container.BindFactory<Vector3, WeaponType, WeaponLootView, LootFactory>().FromFactory<CustomLootFactory>()
                .NonLazy();
            Container.BindFactory<Vector3, EnemyType, IEnemyView, EnemyFactory>().FromFactory<CustomEnemyFactory>()
                .NonLazy();
            
        }

        private void InstallGameStateMachine()
        {
            var gStateMachine = new GameStateMachine();
            Container.BindInterfacesTo<GameStateMachine>().FromInstance(gStateMachine).AsSingle().NonLazy();

            gStateMachine.AddState(Container.Instantiate<LoadingGState>());
            gStateMachine.AddState(Container.Instantiate<MainMenuGState>());
            gStateMachine.AddState(Container.Instantiate<GameGState>());
        }

        private void InstallContainers()
        {
            var cc = new ConfigContainer();
            foreach (var container in containers)
                cc.Add(container);
            
            Container.BindInterfacesAndSelfTo<ConfigContainer>().FromInstance(cc).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<UIContainer>().AsSingle().NonLazy();

        }

        public override void InstallBindings()
        {
            InstallContainers();
            InstallGameStateMachine();
            
            InstallServices();
            InstallFabrics();
            InstallControllers();

            InstallPools();
            Container.BindInterfacesAndSelfTo<ProjectInstaller>().FromInstance(this).AsSingle();


        }

    }
}