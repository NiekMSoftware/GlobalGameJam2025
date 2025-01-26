using UnityEngine;

namespace Bubble
{
    public class BaseScripieShooting : MonoBehaviour
    {
        public GameObject BulletPrefab;
        public float Velocity = 50;
        public float ShootingCooldown = 0.5f;
        public bool mayShootAtStart;
        public bool isEnemy;

        protected Vector2 direction;
        protected Vector3 shootPos;

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
            GameObject _Projectile = Instantiate(BulletPrefab, shootPos, transform.rotation);
            Projectile projectileScript = _Projectile.GetComponent<Projectile>();
            //projectileScript.isEnemyBullet = isEnemy;
            Rigidbody2D ProjectileRB = _Projectile.GetComponent<Rigidbody2D>();
            ProjectileRB.AddForce(direction * Velocity, ForceMode2D.Impulse);

            projectileScript.Owner = gameObject;
        }

        protected bool MayShoot()
        {
            return shootingTimer <= 0;
        }
    }
}
