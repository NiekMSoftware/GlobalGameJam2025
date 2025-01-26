using UnityEngine;
using UnityEngine.AI;

namespace Bubble.Enemies
{
    public class TP_Enemy : GenericAhEnemy
    {
        [SerializeField] private float teleportationRange;
        [SerializeField] private float tpCooldown;

        [SerializeField] private SpriteRenderer SpriteRenderer;
        [SerializeField] private Sprite normalSprite;
        [SerializeField] private Color normalColor;
        [SerializeField] private Sprite vulnerableSprite;
        [SerializeField] private Color vulnerableColor;

        [SerializeField] private GameObject TPEffect;
        [SerializeField] private GameObject TPLineEffect;

        private GameObject spawnedTPEffect;
        private GameObject spawnedTPLineEffect;

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
                SpriteRenderer.sprite = vulnerableSprite;
                SpriteRenderer.color = vulnerableColor;
                print("Teleporting");
                _tpTimer -= Time.deltaTime;
                if (_tpTimer <= 0)
                {
                    SpriteRenderer.sprite = normalSprite;
                    SpriteRenderer.color = normalColor;
                    _teleporting = false;
                    _tpTimer = tpCooldown;
                }
            }
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D (other);
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
            spawnedTPEffect = Instantiate(TPEffect, transform.position, Quaternion.identity);


            Vector2 firePointPosition = pos.position;
            Vector2 backDirection = pos.right * teleportationRange;

            var angle = Mathf.Atan2(backDirection.y, backDirection.x) * Mathf.Rad2Deg;
            spawnedTPLineEffect = Instantiate(TPLineEffect, (Vector2)transform.position, Quaternion.AngleAxis(angle - 90, Vector3.forward));

            Vector2 newPos = firePointPosition + backDirection;
            transform.position = newPos;

            agent.SamplePathPosition(NavMesh.AllAreas, Mathf.Infinity, out NavMeshHit hit);

            transform.position = hit.position;
        }
    }
}
