using UnityEngine;
using UnityEngine.UI;

namespace Bubble
{
    public class UIManager : MonoBehaviour
    {
        public Slider healthSlider;
        public Player player;

        private void OnEnable()
        {
            healthSlider.maxValue = player.MaxHealth;
        }

        private void Update()
        {
            healthSlider.value = player.Health;

            //if (Input.GetKeyDown(KeyCode.Escape))
            //{
            //    #if UNITY_EDITOR
            //        UnityEditor.EditorApplication.isPlaying = false;
            //    #else 
            //        Application.Quit();
            //    #endif
            //}
        }
    }
}
