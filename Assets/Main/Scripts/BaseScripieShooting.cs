using UnityEngine;

namespace Bubble
{
    public class BaseScripieShooting : MonoBehaviour
    {
        public Transform FirePoint;
        public GameObject BulletPrefab;
        public float Velocity = 100;
        public float ShootingCooldown = 0.5f;
        public bool mayShootAtStart;

        protected Vector2 direction;

        private float shootingTimer;

        protected virtual void OnEnable()
        {
            if (!mayShootAtStart) shootingTimer = ShootingCooldown;
        }

        protected virtual void Update()
        {
            shootingTimer -= Time.deltaTime;
        }

        protected virtual void Shoot()
        {
            shootingTimer = ShootingCooldown;
            GameObject Projectile = Instantiate(BulletPrefab, FirePoint.position, Quaternion.identity);
            Rigidbody2D ProjectileRB = Projectile.GetComponent<Rigidbody2D>();
            ProjectileRB.AddForce(direction * Velocity, ForceMode2D.Impulse);
        }

        protected bool MayShoot()
        {
            return shootingTimer <= 0;
        }
    }
}
