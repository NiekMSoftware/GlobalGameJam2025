using Bubble.Utils;
using System;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

namespace Bubble
{
    public class PlayerShoot : BaseScripieShooting
    {
        public RectTransform cursor;

        public Transform FirePoint;
        public Transform ShootPoint;
        public CameraShake cameraShake;
        public float cameraShakeLength = 0.5f;
        public float cameraShakeIntensity =  0.5f;

        private PIM pWayerInpUWUt;

        Vector3 lookTarget;

        private float LookInputX, LookInputY;

        void Start() 
        {
            //pWayerInpUWUt = GetComponentInParent<PlayerInput>();
            // Cursor.lockState = CursorLockMode.Locked;
        }


        protected override void Update()
        {
            base.Update();

            direction = Quaternion.Euler(0, 0, 0) * -FirePoint.right;
            lookTarget = new(LookInputX, LookInputY, FirePoint.position.z);

            //Rotate with mouse or with controller input 
            if (pWayerInpUWUt.KeyboardActive)
                FirePoint.LookAt(Camera.main.ScreenToWorldPoint(lookTarget), Vector3.back);
            else 
                FirePoint.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(LookInputX, -LookInputY) * Mathf.Rad2Deg);

            // Optional adjustment to align FirePoint visually
            FirePoint.localEulerAngles += new Vector3(0, 0, 90);
        }

        private void Look(Vector2 obj)
        {
            LookInputX = obj.x;
            LookInputY = obj.y;
            Debug.LogWarning($"{LookInputX} {LookInputY}");
        }

        protected override void Shoot()
        {
            if (!MayShoot()) return;

            shootPos = ShootPoint.position;

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
