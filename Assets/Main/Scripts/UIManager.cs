using System;
using Bubble.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Bubble
{
    public class UIManager : MonoBehaviour
    {
        public Slider healthSlider;
        public TMP_Text shotsFiredText;
        public Player player;

        private GameManager _gm;

        private void OnEnable()
        {
            healthSlider.maxValue = player.MaxHealth;
        }

        private void Start()
        {
            _gm = GetComponent<GameManager>();
            if (_gm.IsEndless)
                shotsFiredText.gameObject.SetActive(false);
        }

        private void Update()
        {
            healthSlider.value = player.Health;

            shotsFiredText.text = $"Shots fired: {_gm.GetBulletsShot()}";
        }
    }
}
