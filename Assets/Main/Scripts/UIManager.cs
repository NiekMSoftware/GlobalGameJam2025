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
        }
    }
}
