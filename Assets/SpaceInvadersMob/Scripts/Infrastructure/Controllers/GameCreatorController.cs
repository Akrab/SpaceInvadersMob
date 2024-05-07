using SpaceInvadersMob.Infrastructure.Installers;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace SpaceInvadersMob.Infrastructure.Controllers
{
    public class GameCreatorController
    {
        [Inject] private DiContainer _diContainer;

        public GameCreatorController()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name != CONSTANTS.GAME_SCENE) return;

            using var pooledObject = UnityEngine.Pool.ListPool<GameObject>.Get(out var rootGameObjects);
            scene.GetRootGameObjects(rootGameObjects);

       
            foreach (var obj in rootGameObjects)
            {
                var gameInit = obj.GetComponent<GameInit>();
                if (gameInit == null) continue;
                
                _diContainer.Inject(gameInit);
                gameInit.Create();
                
                rootGameObjects.Clear();
                return;

            }
        }

    }
}