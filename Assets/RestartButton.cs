using UnityEngine;
using UnityEngine.SceneManagement;

namespace Bubble
{
    public class RestartButton : MonoBehaviour
    {
        public void RestartLevel()
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));
            Time.timeScale = 1.0f;
        }

        public void MainMenu()
        {
            SceneManager.LoadScene(1);
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(1));
        }
    }
}
