using UnityEngine;

namespace Bubble
{
    public class Player : MonoBehaviour
    {
        public float Health;
        public float MaxHealth;

        public AudioSource AudioSource;

        public AudioClip[] hitSounds;
        public AudioClip[] deathSounds;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.TryGetComponent(out Projectile bullet))
            {
                RemoveHealth(bullet.damage);
            }
        }

        public void RemoveHealth(float amount)
        {
            Health -= amount;

            Health = Mathf.Clamp(Health, 0, MaxHealth);

            if (Health <= 0)
            {
                PlayRandomSoundFromList(deathSounds);
                print("You lost!!!");
            }
            else
            {
                PlayRandomSoundFromList(hitSounds);
            }
        }

        public void AddHealth(float amount)
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
