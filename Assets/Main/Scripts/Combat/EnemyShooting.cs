using System;
using Bubble.Utils;
using UnityEngine;
using UnityEngine.Audio;

namespace Bubble
{
    public class EnemyShooting : BaseScripieShooting
    {
        public float range;

        public Transform target;

        private float LookInputX, LookInputY;
        public AudioSource audioSource;

        public AudioClip[] shootSounds;

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        private void Start()
        {
            target = FindFirstObjectByType<Player>().transform;
        }

        protected override bool MayShoot()
        {
            if (Vector2.Distance(transform.position, target.position) > range) return false;

            return base.MayShoot();
        }

        protected override void Update()
        {
            base.Update();

            shootPos = transform.position;

            direction = target.position - transform.position;

            direction.Normalize();

            if (MayShoot()) { Shoot(); PlayRandomSoundFromList(shootSounds); }
        }


        private void Look(Vector2 obj)
        {
            LookInputX = obj.x;
            LookInputY = obj.y;
        }

        private void PlayRandomSoundFromList(AudioClip[] list)
        {
            AudioClip audioClip = list[UnityEngine.Random.Range(0, list.Length)];
            audioSource.clip = audioClip;
            audioSource.PlayOneShot(audioClip);
        }

        //private void OnEnable()// Subscribe function to events
        //{
        //    pWayerInpUWUt = GetComponentInParent<PIM>();

        //    pWayerInpUWUt.BasicAttackEvent += Shoot;
        //    pWayerInpUWUt.LookEvent += Look;
        //}

        //private void OnDisable()// Unsubscribe function from events
        //{
        //    pWayerInpUWUt.BasicAttackEvent -= Shoot;
        //    pWayerInpUWUt.LookEvent -= Look;

        //}
        //private void OnDestroy()
        //{
        //    pWayerInpUWUt.BasicAttackEvent -= Shoot;
        //    pWayerInpUWUt.LookEvent -= Look;
        //}
    }
}
