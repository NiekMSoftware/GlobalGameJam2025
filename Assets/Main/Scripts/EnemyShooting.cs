using Bubble.Utils;
using UnityEngine;

namespace Bubble
{
    public class EnemyShooting : BaseScripieShooting
    {
        private float LookInputX, LookInputY;


        private void Update()
        {
            //direction = Quaternion.Euler(0, 0, 0) * -FirePoint.right;

            //Vector3 lookTarget = new Vector3(LookInputX, LookInputY, FirePoint.position.z);

            //FirePoint.LookAt(Camera.main.ScreenToWorldPoint(lookTarget), Vector3.back);
            //FirePoint.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z + 90);

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
