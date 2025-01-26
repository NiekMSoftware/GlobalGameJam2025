using UnityEngine;
using UnityEngine.SceneManagement;

namespace Bubble
{
    public class MarinusButtonus : MonoBehaviour
    {
        public Scene Level01;
        public Scene Level02;
        public Scene StartingScreen;
        
        public void RestartLevel()
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));
            Time.timeScale = 1.0f;
        }

        public void MainMenu()
        {
            SceneManager.LoadScene(StartingScreen.buildIndex);
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(StartingScreen.buildIndex));
        }

        public void StartGame()
        {
            SceneManager.LoadSceneAsync(Level01.buildIndex);
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(Level01.buildIndex));
        }

        public void GoToLevel2()
        {
            SceneManager.LoadSceneAsync(Level02.buildIndex);
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(Level02.buildIndex));
        }
    }
}
