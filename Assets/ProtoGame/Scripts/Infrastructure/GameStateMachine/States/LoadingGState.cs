using UnityEngine.SceneManagement;
using Zenject;

namespace ProtoGame.Infrastructure.States
{
    public class LoadingGState : IGState
    {

        public void Enter(object data = null)
        {        
            SceneManager.LoadScene(CONSTANTS.UI_SCENE, LoadSceneMode.Additive);
        }

        public void Exit()
        {

        }
    }
}
