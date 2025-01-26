using UnityEngine;
using UnityEngine.SceneManagement;

namespace Bubble
{
    public class RestartButton : MonoBehaviour
    {
        public void RestartLevel()
        { 
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
