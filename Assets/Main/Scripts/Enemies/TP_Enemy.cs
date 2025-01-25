using Bubble.Temp;
using UnityEngine;

namespace Bubble.Enemies
{
    public class TP_Enemy : GenericAhEnemy
    {
        [SerializeField] private Transform playerFirePoint;
        [SerializeField] private float teleportationRange;
        [SerializeField] private float tpCooldown;
        [SerializeField] private Melee melee;
        [SerializeField] private float meleeCooldown;

        private float meleeTimer;
        private bool meleeTimerStart;
        
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

            if (meleeTimerStart)
            {
                meleeTimer -= Time.deltaTime;

                if (meleeTimer <= 0)
                {
                    melee.MeleeAttack();
                    meleeTimerStart = false;
                    meleeTimer = meleeCooldown;
                }
            }
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Bullet") && !_teleporting && !other.GetComponent<Projectile>().hasHitEnemy)
            {
                other.GetComponent<Projectile>().Owner.TryGetComponent(out PlayerShoot player);
                other.GetComponent<Projectile>().Owner.TryGetComponent(out GenericAhEnemy enemy);

                other.GetComponent<Projectile>().hasHitEnemy = true;
                _teleporting = true;
                _tpTimer = tpCooldown;
                //print("Owner: " + other.GetComponent<Projectile>().Owner);
                //print("Player: " + player);
                //print("Enemy: " + enemy);
                TeleportBehindTarget(player ? player.transform : enemy.transform);
            }
        }
        
        private void TeleportBehindTarget(Transform pos)
        {
            meleeTimer = meleeCooldown;
            meleeTimerStart = true;

            Vector2 firePointPosition = pos.position;
            Vector2 backDirection = pos.right * teleportationRange;
            
            Vector2 newPos = firePointPosition + backDirection;
            transform.position = newPos;
        }
    }
}
