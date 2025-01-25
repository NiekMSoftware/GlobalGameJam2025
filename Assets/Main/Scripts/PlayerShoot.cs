using Bubble.Utils;
using UnityEngine;

namespace Bubble
{
    public class PlayerShoot : MonoBehaviour
    {
        public Transform FirePoint;
        public GameObject BulletPrefab;
        public float Velocity = 100;
        public CameraShake cameraShake;

        private Vector2 direction;
        private PIM pWayerInpUWUt;

        private float LookInputX, LookInputY;

        private Rigidbody2D ProjectileRB;
        void Start() 
        {
            //pWayerInpUWUt = GetComponentInParent<PlayerInput>();
            Cursor.lockState = CursorLockMode.Confined;
        }


        private void Update()
        {
            direction = Quaternion.Euler(0, 0, 0) * -FirePoint.right;
            
            Vector3 lookTarget = new Vector3(LookInputX, LookInputY, FirePoint.position.z);
            
            FirePoint.LookAt(Camera.main.ScreenToWorldPoint(lookTarget), Vector3.back);
            FirePoint.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z + 90);
        }


        private void Look(Vector2 obj)
        {
            LookInputX = obj.x;
            LookInputY = obj.y;
        }

        private void Shoot()
        {
            GameObject Projectile = Instantiate(BulletPrefab, FirePoint.position, Quaternion.identity);
            ProjectileRB = Projectile.GetComponent<Rigidbody2D>();
            ProjectileRB.AddForce(direction * Velocity, ForceMode2D.Impulse);

            cameraShake.Shake(3, 1);
        }

        private void OnEnable()// Subscribe function to events
        {
            pWayerInpUWUt = GetComponentInParent<PIM>();

            pWayerInpUWUt.BasicAttackEvent += Shoot;
            pWayerInpUWUt.LookEvent += Look;
        }

        private void OnDisable()// Unsubscribe function from events
        {
            pWayerInpUWUt.BasicAttackEvent -= Shoot;
            pWayerInpUWUt.LookEvent -= Look;

        }
        private void OnDestroy()
        {
            pWayerInpUWUt.BasicAttackEvent -= Shoot;
            pWayerInpUWUt.LookEvent -= Look;
        }
    }
}
