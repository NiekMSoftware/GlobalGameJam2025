using Bubble.Temp;
using UnityEngine;

namespace Bubble.Enemies
{
    public class TP_Enemy : GenericAhEnemy
    {
        [SerializeField] private Transform playerFirePoint;
        [SerializeField] private float teleportationRange;
        
        private bool teleporting;

        protected override void OnValidate()
        {
            base.OnValidate();

            playerFirePoint = target.GetComponent<PC_TopDown>().GetComponentInChildren<PlayerShoot>().transform;
        }

        protected override void Start()
        {
            base.Start();
            playerFirePoint = target.GetComponent<PC_TopDown>().GetComponentInChildren<PlayerShoot>().transform;
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Bullet"))
            {
                teleporting = true;
                TeleportBehindTarget();
            }
        }
        
        private void TeleportBehindTarget()
        {
            Vector2 firePointPosition = playerFirePoint.position;
            Vector2 backDirection = playerFirePoint.right * teleportationRange;
            
            Vector2 newPos = firePointPosition + backDirection;
            transform.position = newPos;
        }
    }
}
