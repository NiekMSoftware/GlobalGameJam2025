using Bubble.Enemies;
using System.Collections;
using UnityEngine;

namespace Bubble
{
    public class Player : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;

        public int DamageInTrigger;

        public int Health;
        public int MaxHealth;
    
        public GameObject DeadPanel;
        public AudioSource audioSource;

        public AudioClip[] hitSounds;
        public AudioClip[] deathSounds;

        private bool mayDamage = true;

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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out GenericAhEnemy enemy))
            {
                RemoveHealth(DamageInTrigger);
            }
        }

        public void RemoveHealth(int amount)
        {
            if (!mayDamage) return;

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

            StartCoroutine(nameof(BlinkPlayer));
        }

        private IEnumerator BlinkPlayer()
        {
            mayDamage = false;

            float waitAmount = 0.2f;

            spriteRenderer.enabled = false;

            yield return new WaitForSeconds(waitAmount);

            spriteRenderer.enabled = true;

            yield return new WaitForSeconds(waitAmount);

            spriteRenderer.enabled = false;

            yield return new WaitForSeconds(waitAmount);

            spriteRenderer.enabled = true;

            mayDamage = true;
        }

        public void AddHealth(int amount)
        {
            Health += amount;

            Health = Mathf.Clamp(Health, 0, MaxHealth);
        }

        private void PlayRandomSoundFromList(AudioClip[] list)
        {
            AudioClip audioClip = list[Random.Range(0, list.Length)];
            audioSource.clip = audioClip;
            audioSource.PlayOneShot(audioClip);
        }
    }
}
