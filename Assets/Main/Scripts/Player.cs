using UnityEngine;

namespace Bubble
{
    public class Player : MonoBehaviour
    {
        public int Health;
        public int MaxHealth;
    
        public GameObject DeadPanel;
        public AudioSource AudioSource;

        public AudioClip[] hitSounds;
        public AudioClip[] deathSounds;


        private void Start()
        {
            DeadPanel.SetActive(false);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.TryGetComponent(out Projectile bullet))
            {
                RemoveHealth((int)bullet.damage);
            }
        }

        public void RemoveHealth(int amount)
        {
            Health -= amount;

            Health = Mathf.Clamp(Health, -1, MaxHealth);
            Debug.Log($"{Health}");

            if (Health <= 0)
            {
                Debug.Log("You lost!!!");
                DeadPanel.SetActive(true);
                Time.timeScale = 0;
                PlayRandomSoundFromList(deathSounds);
            }
            else
            {
                PlayRandomSoundFromList(hitSounds);
            }
        }

        public void AddHealth(int amount)
        {
            Health += amount;

            Health = Mathf.Clamp(Health, 0, MaxHealth);
        }

        private void PlayRandomSoundFromList(AudioClip[] list)
        {
            AudioClip audioClip = list[Random.Range(0, list.Length)];
            AudioSource.clip = audioClip;
            AudioSource.PlayOneShot(audioClip);
        }
    }
}
