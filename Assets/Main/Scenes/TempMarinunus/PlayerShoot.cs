using UnityEngine;

namespace Bubble
{
    public class PlayerShoot : MonoBehaviour
    {
        public Transform FirePoint;
        public GameObject BulletPrefab;

        private Vector2 direction;
        public float Velocity = 100;


        void Start() 
        {
            //Shoot();
            Cursor.lockState = CursorLockMode.Confined;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0)) 
                Shoot();

            direction = Quaternion.Euler(0, 0, 0) * FirePoint.up;

            //transform.LookAt(Camera.main.ScreenToWorldPoint(new Vector2(0,2)), Vector3.back);
            FirePoint.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.back);
            FirePoint.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z - 180);
        }

        private void Shoot()
        {
            GameObject Projectile = Instantiate(BulletPrefab, FirePoint.position, Quaternion.identity);
            Rigidbody2D rb = Projectile.GetComponent<Rigidbody2D>();
            rb.AddForce(direction * Velocity, ForceMode2D.Impulse);
        }
    }
}
