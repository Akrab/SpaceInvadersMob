using UnityEngine.SceneManagement;

namespace SpaceInvadersMob.Infrastructure.States
{
    public class LoadingGState : IGState
    {

        public void Enter(object data = null)
        {        
            SceneManager.LoadSceneAsync(CONSTANTS.UI_SCENE, LoadSceneMode.Additive);
            //     .ToObservable()
            //     .Do(x => Debug.Log(x)) // output progress
            //     .Last() // last sequence is load completed
            //     .Subscribe();
            // SceneManager.LoadScene(CONSTANTS.UI_SCENE, LoadSceneMode.Additive);
        }

        public void Exit()
        {

        }
    }
}
