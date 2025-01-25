using UnityEngine;

namespace Bubble
{
    public class Player : MonoBehaviour
    {
        public float Health;
        public float MaxHealth;

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
                print("You lost!!!");
            }
        }

        public void AddHealth(float amount)
        {
            Health += amount;

            Health = Mathf.Clamp(Health, 0, MaxHealth);
        }
    }
}
