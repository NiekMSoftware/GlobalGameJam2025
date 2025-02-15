using Bubble.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Bubble
{
    public class PlayerShoot : BaseScripieShooting
    {
        public Transform Transform;
        public Transform ShootPoint;
        public CameraShake cameraShake;
        public float cameraShakeLength = 0.5f;
        public float cameraShakeIntensity =  0.2f;
        public Image cooldownIndicator;

        private PIM pWayerInpUWUt;
        private Vector3 lookTarget;
        private float LookInputX, LookInputY;

        public AudioSource audioSource;

        public AudioClip[] shootSounds;

        public int ShotsFired { get; private set; }

        protected override void Update()
        {
            base.Update();

            direction = Quaternion.Euler(0, 0, 0) * -Transform.right;
            lookTarget = new(LookInputX, LookInputY, Transform.position.z);

            //Rotate with mouse or with controller input 
            if (pWayerInpUWUt.KeyboardActive)
                Transform.LookAt(Camera.main.ScreenToWorldPoint(lookTarget), Vector3.back);
            else 
                Transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(LookInputX, -LookInputY) * Mathf.Rad2Deg);

            Transform.localEulerAngles += new Vector3(0, 0, 90);

            cooldownIndicator.fillAmount = shootingTimer;
        }

        private void Look(Vector2 obj)
        {
            LookInputX = obj.x;
            LookInputY = obj.y;
        }

        protected override void Shoot()
        {
            if (!MayShoot()) return;
            ShotsFired++;

            shootPos = ShootPoint.position;
            PlayRandomSoundFromList(shootSounds);
            base.Shoot();
            cameraShake.Shake(cameraShakeLength, cameraShakeIntensity);
        }


        protected override void OnEnable()// Subscribe function to events
        {
            base.OnEnable();

            Transform = transform;

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
        private void PlayRandomSoundFromList(AudioClip[] list)
        {
            AudioClip audioClip = list[Random.Range(0, list.Length)];
            audioSource.clip = audioClip;
            audioSource.PlayOneShot(audioClip);
        }
    }
}
