using UnityEngine;

namespace Bubble
{
    public class PlayerShoot : MonoBehaviour
    {
        public Transform FirePoint;
        public GameObject BulletPrefab;

        private Vector3 direction;
        private float Velocity = 100;


        void Start() 
        {
            //Shoot();
        }

        private void Update()
        { 
            if (Input.GetKeyDown(KeyCode.Mouse0)) Shoot();

            Vector2 direction = Quaternion.Euler(0, 0, 0) * FirePoint.forward;
        }

        private void Shoot()
        {
            GameObject Projectile = Instantiate(BulletPrefab,FirePoint.position, Quaternion.identity);
            Rigidbody2D rb = Projectile.GetComponent<Rigidbody2D>();
            rb.AddForce(direction * Velocity, ForceMode2D.Impulse);
        }
    }
}
