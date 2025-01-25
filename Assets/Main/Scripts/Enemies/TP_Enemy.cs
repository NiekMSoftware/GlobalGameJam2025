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
        }

        protected override void Start()
        {
            base.Start();
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
                other.GetComponent<Projectile>().Owner.TryGetComponent(out PlayerShoot player);
                other.GetComponent<Projectile>().Owner.TryGetComponent(out Enemy enemy);

                _teleporting = true;
                _tpTimer = tpCooldown;
                TeleportBehindTarget(player ? player.transform : enemy.transform);
            }
        }
        
        private void TeleportBehindTarget(Transform pos)
        {
            Vector2 firePointPosition = pos.position;
            Vector2 backDirection = pos.right * teleportationRange;
            
            Vector2 newPos = firePointPosition + backDirection;
            transform.position = newPos;
        }
    }
}
