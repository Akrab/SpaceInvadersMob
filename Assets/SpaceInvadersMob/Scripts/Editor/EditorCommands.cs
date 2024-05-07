using UnityEditor;
using UnityEditor.SceneManagement;

namespace SpaceInvadersMob.Editor
{
    public class EditorCommands : UnityEditor.Editor
    {
        [MenuItem("SpaceInvadersMob/Play")]
        private static void Play()
        {
            EditorSceneManager.OpenScene("Assets/SpaceInvadersMob/Scenes/Lobby.unity");
            EditorApplication.isPlaying = true;
        }
        
        [MenuItem("SpaceInvadersMob/Load Game")]
        private static void LoadGame()
        {
            EditorSceneManager.OpenScene("Assets/SpaceInvadersMob/Scenes/Game.unity");
            EditorApplication.isPlaying = false;
        }

        [MenuItem("SpaceInvadersMob/Load Lobby")]
        private static void LoadLobby()
        {
            EditorSceneManager.OpenScene("Assets/SpaceInvadersMob/Scenes/Lobby.unity");
            EditorApplication.isPlaying = false;
        }

        [MenuItem("SpaceInvadersMob/Load UI")]
        private static void LoadUI()
        {
            EditorSceneManager.OpenScene("Assets/SpaceInvadersMob/Scenes/UI.unity");
            EditorApplication.isPlaying = false;
        }

        [MenuItem("SpaceInvadersMob/Load Loading")]
        private static void LoadLoading()
        {
            EditorSceneManager.OpenScene("Assets/SpaceInvadersMob/Scenes/Loading.unity");
            EditorApplication.isPlaying = false;
        }

    }
}
