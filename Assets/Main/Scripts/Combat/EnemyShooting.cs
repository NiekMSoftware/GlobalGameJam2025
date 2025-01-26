using System;
using Bubble.Utils;
using UnityEngine;

namespace Bubble
{
    public class EnemyShooting : BaseScripieShooting
    {
        public Transform target;

        private float LookInputX, LookInputY;

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        private void Start()
        {
            target = FindFirstObjectByType<Player>().transform;
        }

        protected override void Update()
        {
            base.Update();

            shootPos = transform.position;

            direction = target.position - transform.position;

            direction.Normalize();

            if (MayShoot()) Shoot();
        }


        private void Look(Vector2 obj)
        {
            LookInputX = obj.x;
            LookInputY = obj.y;
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
