using Bubble.Utils;
using UnityEngine;

namespace Bubble
{
    public class PlayerShoot : BaseScripieShooting
    {
        public CameraShake cameraShake;
        public float cameraShakeLength = 0.5f;
        public float cameraShakeIntensity =  0.5f;

        private PIM pWayerInpUWUt;

        private float LookInputX, LookInputY;

        void Start() 
        {
            //pWayerInpUWUt = GetComponentInParent<PlayerInput>();
            Cursor.lockState = CursorLockMode.Confined;
        }


        protected override void Update()
        {
            base.Update();

            direction = Quaternion.Euler(0, 0, 0) * -FirePoint.right;
            
            Vector3 lookTarget = new(LookInputX, LookInputY, FirePoint.position.z);
            
            FirePoint.LookAt(Camera.main.ScreenToWorldPoint(lookTarget), Vector3.back);
            FirePoint.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z + 90);
        }


        private void Look(Vector2 obj)
        {
            LookInputX = obj.x;
            LookInputY = obj.y;
        }

        protected override void Shoot()
        {
            if (!MayShoot()) return;

            base.Shoot();

            cameraShake.Shake(cameraShakeLength, cameraShakeIntensity);
        }

        protected override void OnEnable()// Subscribe function to events
        {
            base.OnEnable();

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
