using Bubble.Temp;
using UnityEngine;

namespace Bubble.Enemies
{
    public class TP_Enemy : GenericAhEnemy
    {
        [SerializeField] private Transform playerFirePoint;
        [SerializeField] private float teleportationRange;
        [SerializeField] private float tpCooldown;

        private float _tpTimer;
        private bool _teleporting;

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

        protected override void Update()
        {
            base.Update();
            
            if (_teleporting)
            {
                print("Teleporting");
                _tpTimer -= Time.deltaTime;
                if (_tpTimer <= 0)
                {
                    _teleporting = false;
                    _tpTimer = tpCooldown;
                }
            }
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Bullet") && !_teleporting)
            {
                _teleporting = true;
                _tpTimer = tpCooldown;
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
