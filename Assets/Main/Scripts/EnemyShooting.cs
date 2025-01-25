using Bubble.Utils;
using UnityEngine;

namespace Bubble
{
    public class EnemyShooting : BaseScripieShooting
    {
        public Transform target;

        private float LookInputX, LookInputY;

        //protected override void OnEnable()
        //{
        //    base.OnEnable();
        //}


        protected override void Update()
        {
            base.Update();

            shootPos = transform.position;

            direction = target.position - transform.position;

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
